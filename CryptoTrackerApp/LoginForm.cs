using System;
using System.Windows.Forms;
using Supabase;

namespace CryptoTrackerApp
{
    public partial class LoginForm : Form
    {
        private Client supabaseClient;

        public LoginForm()
        {
           // InitializeComponent();
            // Configura el cliente de Supabase
            string url = "https://your-project-url.supabase.co";
            string key = "your-api-key";
            supabaseClient = new Client(url, key);
            supabaseClient.InitializeAsync();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var email = txtUsername.Text;
            var password = txtPassword.Text;

            try
            {
                var response = await supabaseClient.Auth.SignIn(email, password); //ver sign in
                if (response.User != null)
                {
                  //  MainForm mainForm = new MainForm();
                  //  mainForm.Show();
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