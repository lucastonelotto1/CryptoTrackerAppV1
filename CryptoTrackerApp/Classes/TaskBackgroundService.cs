using Supabase.Gotrue;
using Supabase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptoTrackerApp.Classes;


namespace CryptoTrackerApp.Classes
{
    public partial class TaskBackgroundService
    {
        private CoinCapApiClient apiClient;
        private Supabase.Client supabaseClient;
        private EmailService emailService;
        private string userId;
        private string email;
        private string name;

        public async Task RunBackgroundTaskAsync(Session session)
        {
            try
            {
                userId = session.User.Id;
                email = session.User.Email;
                name = "User";

                apiClient = new CoinCapApiClient();
                emailService = new EmailService();
                string url = "https://cjulheqhpurkozgepnja.supabase.co";
                string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImNqdWxoZXFocHVya296Z2VwbmphIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTcxOTk2MTA5MiwiZXhwIjoyMDM1NTM3MDkyfQ.K_Xbt0gItJ9U3NFFYlKk-_n-a98GNsFVB4BwCymRbck";
                supabaseClient = new Supabase.Client(url, key);
                await supabaseClient.InitializeAsync();
                //MessageBox.Show("Inicialización de Supabase completada.");

                while (true)
                {
                    //MessageBox.Show("Iniciando ciclo de tarea de fondo.");

                    Guid userIdGuid;
                    if (!Guid.TryParse(userId, out userIdGuid))
                    {
                        MessageBox.Show("Invalid user ID format.");
                        return;
                    }
                    // Obtén la lista de criptomonedas favoritas del usuario desde Supabase
                    var favoriteCryptos = await supabaseClient
                        .From<FavoriteCryptos>()
                        .Where(x => x.UserId == userIdGuid)
                        .Get();

                    //MessageBox.Show("Favoritos obtenidos: " + favoriteCryptos.Models.Count.ToString());

                    // Obtener la lista de criptoactivos desde CoinCap
                    List<CryptoAsset> cryptoAssets = await apiClient.GetCryptoAssetsAsync();
                    //MessageBox.Show("Criptoactivos obtenidos: " + cryptoAssets.Count.ToString());

                    foreach (var favorite in favoriteCryptos.Models)
                    {
                        var matchingCrypto = cryptoAssets.FirstOrDefault(c => c.Symbol == favorite.CryptoId);
                        if (matchingCrypto != null)
                        {
                            float changePercent24Hr = Math.Abs((float)matchingCrypto.ChangePercent24Hr);
                            //MessageBox.Show($"La criptomoneda {matchingCrypto.Name} ha cambiado un {changePercent24Hr}% en las últimas 24 horas.");

                            if (changePercent24Hr > favorite.Limit)
                            {
                                //MessageBox.Show($"La criptomoneda {matchingCrypto.Name} ha superado el límite establecido. Cambio en 24 horas: {changePercent24Hr}%");
                                await emailService.SendEmailAsync(
                                    email,
                                    name,
                                    $"The cryptocurrency {matchingCrypto.Name} has changed by {matchingCrypto.ChangePercent24Hr}% in the last 24 hours.",
                                    $"<h1>The cryptocurrency {matchingCrypto.Name} has changed by {matchingCrypto.ChangePercent24Hr}% in the last 24 hours.</h1>"
                                );
                                DateTime now = DateTime.Now;
                                string argentinaTime = now.ToString("dd/MM/yyyy HH:mm");

                                var newAlert = new AlertsHistory
                                {
                                    UserId = userIdGuid,
                                    CryptoIdOutOfLimit = matchingCrypto.Name,
                                    ChangePercent = (float)matchingCrypto.ChangePercent24Hr,
                                    Time = argentinaTime
                                };

                                // Inserta la nueva entrada en la base de datos
                                var insertResponse = await supabaseClient
                                    .From<AlertsHistory>()
                                    .Insert(newAlert);
                            }
                        }
                    }

                    // Espera 5 minutos
                    await Task.Delay(TimeSpan.FromHours(1));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la tarea de fondo: " + ex.Message);
            }
        }
    }
}
