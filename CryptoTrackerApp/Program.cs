using CryptoTrackerApp.DataAccessLayer.EntityFrameWork;
using CryptoTrackerApp;
using CryptoTrackerApp.Api;
using CryptoTrackerApp.EmailServices;
using CryptoTrackerApp.DataAccessLayer;
using Microsoft.Extensions.Configuration;

namespace CryptoTrackerApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Ruta al archivo de configuración
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.txt");

            // Cargar la configuración desde el archivo
            var databaseConfig = DatabaseConfig.Load(configPath);

            // Crear el cliente Supabase usando la configuración cargada
            var supabaseClient = DatabaseHelper.CreateClient(databaseConfig);

            // Inicializa las dependencias
            IRepository repository = new Repository(supabaseClient); // Implementación concreta
            ICoinCapApiClient cryptoApiClient = new CoinCapApiClient(); // Implementación concreta
            IEmailService emailService = new EmailService(); // Implementación concreta

            // Crear la fachada con las dependencias
            FacadeCT facadeCT = new FacadeCT(repository, cryptoApiClient, emailService);

            // Inicializa la configuración de la aplicación
            ApplicationConfiguration.Initialize();

            // Ejecuta el formulario principal de la aplicación, pasando la fachada como parámetro
            Application.Run(new LoginForm(facadeCT));
        }
    }
}
