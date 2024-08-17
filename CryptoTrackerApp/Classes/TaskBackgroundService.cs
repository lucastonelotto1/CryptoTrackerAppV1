using NLog;
using Supabase.Gotrue;

namespace CryptoTrackerApp.Classes
{
    public partial class TaskBackgroundService
    {
        private CoinCapApiClient apiClient;
        private DatabaseHelper databaseHelper;
        private EmailService emailService;
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        
        
        private string userId;
        private string email;
        private string name;

        public async Task RunBackgroundTaskAsync(Session session)
        {
            LogManager.LoadConfiguration("nlog.config");
            Logger.Info("Background Task Initialized.");
            try
            {
                userId = session.User.Id;
                email = session.User.Email;
                name = "User";

                apiClient = new CoinCapApiClient();
                emailService = new EmailService();
                databaseHelper = new DatabaseHelper();

                while (true)
                {
                    //MessageBox.Show("Iniciando ciclo de tarea de fondo.");
                    // Obtén la lista de criptomonedas favoritas del usuario desde Supabase
                    var favoriteCryptos = await databaseHelper.GetFavoriteCryptos(userId);
                        

                    // Obtener la lista de criptoactivos desde CoinCap
                    List<CryptoAsset> cryptoAssets = await apiClient.GetCryptoAssetsAsync();
                    //MessageBox.Show("Criptoactivos obtenidos: " + cryptoAssets.Count.ToString());

                    foreach (var favorite in favoriteCryptos)
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

                                // Inserta la nueva entrada en la base de datos
                                databaseHelper.AddAlert(userId, matchingCrypto.Name, (float)matchingCrypto.ChangePercent24Hr, argentinaTime);


                            }
                        }
                    }

                    // Espera 5 minutos
                    await Task.Delay(TimeSpan.FromHours(1));
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An error occurred in the Background Service: " + ex.Message);
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}
