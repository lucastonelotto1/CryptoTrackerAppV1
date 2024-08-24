using CryptoTrackerApp.DataAccessLayer.EntityFrameWork;
using CryptoTrackerApp;

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

            // Crear el repositorio principal y utilizarlo
            var repository = new Repository(supabaseClient);

            

            // Inicializa la configuración de la aplicación
            ApplicationConfiguration.Initialize();

            // Ejecuta el formulario principal de la aplicación
            Application.Run(new LoginForm()); // O MainForm dependiendo de tu lógica

        }
    }
}
