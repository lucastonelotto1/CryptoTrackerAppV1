namespace CryptoTrackerApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Inicializa la configuraci�n de la aplicaci�n
            ApplicationConfiguration.Initialize();

            // Ejecuta el formulario principal de la aplicaci�n
            Application.Run(new LoginForm()); // O MainForm dependiendo de tu l�gica
        }
    }
}