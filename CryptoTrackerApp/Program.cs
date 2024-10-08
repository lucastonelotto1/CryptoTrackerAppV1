using CryptoTrackerApp.DataAccessLayer.EntityFrameWork;
using CryptoTrackerApp;
using CryptoTrackerApp.Api;
using CryptoTrackerApp.EmailServices;
using CryptoTrackerApp.DataAccessLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;



namespace CryptoTrackerApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Inicializa la configuración de la aplicación
            ApplicationConfiguration.Initialize();

            // Crear el cliente Supabase usando la configuración cargada
            var supabaseClient = DatabaseHelper.CreateClient();


            // Cargar la configuración desde el archivo appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();


            // Inicializa las dependencias
            IRepository repository = new Repository(supabaseClient); // Implementación concreta
            ICoinCapApiClient cryptoApiClient = new CoinCapApiClient(); // Implementación concreta
            IEmailService emailService = new EmailService(configuration); // Pasar la configuración aquí

            // Crear la fachada con las dependencias
            FacadeCT facadeCT = new FacadeCT(repository, cryptoApiClient, emailService);



            // Ejecuta el formulario principal de la aplicación
            Application.Run(new LoginForm(facadeCT));
        }
    }
}
