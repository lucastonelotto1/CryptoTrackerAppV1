using CryptoTrackerApp.Classes;
using CryptoTrackerApp.DTO;
using CryptoTrackerApp.EmailService;
using NLog;
using Supabase.Gotrue;

namespace CryptoTrackerApp.Infrastructure
{
    public partial class TaskBackgroundService
    {
        // Instances
        private readonly FacadeCT _facadeCT;
        private readonly IEmailService emailService;
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public TaskBackgroundService(IEmailService emailService, FacadeCT facadeCT)
        {
            this.emailService = emailService;

            //instance
            _facadeCT = facadeCT;
        }

        public async Task RunBackgroundTaskAsync(SessionDTO session)
        {
            LogManager.LoadConfiguration("nlog.config");
            Logger.Info("Background Task Initialized.");

            string userId = session.Id;
            string email = session.Email;
            string name = "User";

            try
            {
                while (true)
                {
                    var favoriteCryptos = await _facadeCT.GetFavoriteCryptos(userId);
                    List<CryptoAsset> cryptoAssets = await _facadeCT.GetCryptoAssetsAsync();

                    foreach (var favorite in favoriteCryptos)
                    {
                        var matchingCrypto = cryptoAssets.FirstOrDefault(c => c.Symbol == favorite.CryptoId);
                        if (matchingCrypto != null)
                        {
                            float changePercent24Hr = Math.Abs((float)matchingCrypto.ChangePercent24Hr);

                            if (changePercent24Hr > favorite.Limit)
                            {
                                await emailService.SendEmailAsync(
                                    email,
                                    name,
                                    $"The cryptocurrency {matchingCrypto.Name} has changed by {matchingCrypto.ChangePercent24Hr}% in the last 24 hours.",
                                    $"<h1>The cryptocurrency {matchingCrypto.Name} has changed by {matchingCrypto.ChangePercent24Hr}% in the last 24 hours.</h1>"
                                );

                                string argentinaTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                                databaseHelper.AddAlert(userId, matchingCrypto.Name, (float)matchingCrypto.ChangePercent24Hr, argentinaTime);
                            }
                        }
                    }

                    await Task.Delay(TimeSpan.FromHours(1));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "An error occurred in the Background Service.");
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}