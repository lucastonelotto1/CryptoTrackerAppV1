namespace CryptoTrackerApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Inicializa la configuración de la aplicación
            ApplicationConfiguration.Initialize();

            // Ejecuta el formulario principal de la aplicación
            Application.Run(new LoginForm()); // O MainForm dependiendo de tu lógica
        }
    }
}