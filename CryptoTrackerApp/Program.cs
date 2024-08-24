using CryptoTrackerApp.DataAccessLayer.EntityFrameWork;
using CryptoTrackerApp;

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

            // Crear el repositorio principal y utilizarlo
            var repository = new Repository(supabaseClient);

            

            // Inicializa la configuraci�n de la aplicaci�n
            ApplicationConfiguration.Initialize();

            // Ejecuta el formulario principal de la aplicaci�n
            Application.Run(new LoginForm()); // O MainForm dependiendo de tu l�gica

        }
    }
}
