using System;
using System.Linq;
using System.Windows.Forms;
using Supabase;
// Agregar el espacio de nombres de MainForm si está en otro namespace
using CryptoTrackerApp;

namespace CryptoTrackerApp
{
    public partial class LoginForm : Form
    {
        private Supabase.Client supabaseClient;

        public LoginForm()
        {
            //InitializeComponent();
            InitializeSupabase();
        }

        private void InitializeSupabase()
        {
            try
            {
                string url = "https://your-project-url.supabase.co";
                string key = "your-api-key";
                supabaseClient = new Supabase.Client(url, key);
                supabaseClient.InitializeAsync().Wait(); // Asegurarse de que la inicialización esté completa
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing Supabase: " + ex.Message);
                // Manejo adecuado si supabaseClient no se inicializa correctamente
                this.Close(); // Cierra la aplicación si no puede inicializar Supabase
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text;
            var password = txtPassword.Text;

            try
            {
                var response = await supabaseClient
                    .From<Client>()
                    .Select(x => new object[] { x.username, x.password })
                    .Where(x => x.username == username)
                    .Where(x => x.password == password)
                    .Single();


                if (response.Models.Any())
                {
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid credentials, please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
