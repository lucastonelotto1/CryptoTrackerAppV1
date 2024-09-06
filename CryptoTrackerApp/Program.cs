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
            // Ruta al archivo de configuraci�n
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.txt");

            // Cargar la configuraci�n desde el archivo
            var databaseConfig = DatabaseConfig.Load(configPath);

            // Crear el cliente Supabase usando la configuraci�n cargada
            var supabaseClient = DatabaseHelper.CreateClient(databaseConfig);

            // Inicializa las dependencias
            IRepository repository = new Repository(supabaseClient); // Implementaci�n concreta
            ICoinCapApiClient cryptoApiClient = new CoinCapApiClient(); // Implementaci�n concreta
            IEmailService emailService = new EmailService(); // Implementaci�n concreta

            // Crear la fachada con las dependencias
            FacadeCT facadeCT = new FacadeCT(repository, cryptoApiClient, emailService);

            // Inicializa la configuraci�n de la aplicaci�n
            ApplicationConfiguration.Initialize();

            // Ejecuta el formulario principal de la aplicaci�n, pasando la fachada como par�metro
            Application.Run(new LoginForm(facadeCT));
        }
    }
}
