using System;
using System.Windows.Forms;
using CryptoTrackerApp.Classes;
using NLog;
using Supabase;

namespace CryptoTrackerApp
{
    public partial class LoginForm : Form
    {
        private DatabaseHelper databaseHelper;
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public LoginForm()
        {
            LogManager.LoadConfiguration("nlog.config");
            Logger.Info("Login Initialized.");
            InitializeComponent();

            // Instances
            databaseHelper = new DatabaseHelper();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var email = txtUsername.Text;
            var password = txtPassword.Text;

            try
            {
                var session = await databaseHelper.Authorize(email, password);

                if (session != null && session.AccessToken != null)
                {
                    // Iniciar la tarea en segundo plano con la sesión
                    TaskBackgroundService taskBackgroundMailService = new TaskBackgroundService();
                    _ = taskBackgroundMailService.RunBackgroundTaskAsync(session);

                    MainForm mainForm = new MainForm(session);
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
                Logger.Error("An error occurred while logging: " + ex.Message);
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}