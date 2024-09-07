using CryptoTrackerApp.DTO;
using CryptoTrackerApp.EmailServices;
using NLog;

namespace CryptoTrackerApp.Infrastructure
{
    public partial class TaskBackgroundService
    {
        private readonly FacadeCT _facadeCT;
        private readonly IEmailService _emailService;
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public TaskBackgroundService(IEmailService emailService, FacadeCT facadeCT)
        {
            _emailService = emailService;
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
                    // Delegar la lógica de monitoreo de cambios y alertas a la fachada
                    await _facadeCT.MonitorCryptoChangesAsync(userId, email, name);

                    // Esperar una hora antes de volver a ejecutar
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