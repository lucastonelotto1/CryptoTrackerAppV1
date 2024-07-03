using System;
using System.Linq;
using System.Windows.Forms;
using Supabase;

namespace CryptoTrackerApp
{
    public partial class LoginForm : Form
    {
        private Supabase.Client supabaseClient;

        public LoginForm()
        {
            InitializeComponent();
            InitializeSupabase();
        }

        private void InitializeSupabase()
        {
            try
            {
                string url = "https://cjulheqhpurkozgepnja.supabase.co";
                string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImNqdWxoZXFocHVya296Z2VwbmphIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTcxOTk2MTA5MiwiZXhwIjoyMDM1NTM3MDkyfQ.K_Xbt0gItJ9U3NFFYlKk-_n-a98GNsFVB4BwCymRbck";
                supabaseClient = new Supabase.Client(url, key);
                supabaseClient.InitializeAsync().Wait(); // Asegurarse de que la inicialización esté completa
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing Supabase: " + ex.Message);
                this.Close(); // Cierra la aplicación si no puede inicializar Supabase
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var username = txtPassword.Text;
            var password = txtUsername.Text;

            try
            {
                var response = await supabaseClient
                    .From<Client>()
                    .Where(x => x.username == username && x.password == password)
                    .Get();

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
