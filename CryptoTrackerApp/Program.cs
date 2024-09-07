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
            // Ruta al archivo de configuraci�n
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.txt");

            // Cargar la configuraci�n desde el archivo
            var databaseConfig = DatabaseConfig.Load(configPath);

            // Crear el cliente Supabase usando la configuraci�n cargada
            var supabaseClient = DatabaseHelper.CreateClient(databaseConfig);


            // Cargar la configuraci�n desde el archivo appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();


            // Inicializa las dependencias
            IRepository repository = new Repository(supabaseClient); // Implementaci�n concreta
            ICoinCapApiClient cryptoApiClient = new CoinCapApiClient(); // Implementaci�n concreta
            IEmailService emailService = new EmailService(configuration); // Pasar la configuraci�n aqu�

            // Crear la fachada con las dependencias
            FacadeCT facadeCT = new FacadeCT(repository, cryptoApiClient, emailService);

            // Inicializa la configuraci�n de la aplicaci�n
            ApplicationConfiguration.Initialize();

            // Ejecuta el formulario principal de la aplicaci�n
            Application.Run(new LoginForm(facadeCT));
        }
    }
}
