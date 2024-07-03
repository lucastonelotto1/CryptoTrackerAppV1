using System;
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
                // No need to initialize Supabase here for authentication purposes
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing Supabase: " + ex.Message);
                this.Close(); // Close the application if Supabase initialization fails
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var email = txtUsername.Text;
            var password = txtPassword.Text;

            try
            {
                var response = await supabaseClient.Auth.SignIn(email, password);

                if (response != null && response.AccessToken != null)
                {
                    string jwtToken = response.AccessToken;
                    // Save jwtToken securely for further use in your application
                    //TokenManager.SaveToken(jwtToken);

                    //MainForm mainForm = new MainForm(jwtToken);
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

