using System;
using System.Windows.Forms;
using CryptoTrackerApp.Infrastructure;
using NLog;
using CryptoTrackerApp.DTO;

namespace CryptoTrackerApp
{
    public partial class LoginForm : Form
    {
        private readonly FacadeCT _facadeCT;
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public LoginForm(FacadeCT facadeCT)
        {
            LogManager.LoadConfiguration("nlog.config");
            Logger.Info("Login Initialized.");
            InitializeComponent();
            _facadeCT = facadeCT;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var email = txtUsername.Text;
            var password = txtPassword.Text;

            try
            {
                // Llamada al facade para manejar la autorización y la tarea en segundo plano
                var sessionDTO = await _facadeCT.AuthorizeAndStartBackgroundTask(email, password);

                if (sessionDTO != null && sessionDTO.AccessToken != null)
                {
                    MainForm mainForm = new MainForm(sessionDTO, _facadeCT);
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
                Logger.Error("An error occurred: " + ex.Message);
                MessageBox.Show("An error occurred. Please try again later.");
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
