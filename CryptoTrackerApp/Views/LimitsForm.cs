using Supabase.Gotrue;
using NLog;
using CryptoTrackerApp.DTO;

namespace CryptoTrackerApp.Views
{
    public partial class LimitsForm : Form
    {
        private string UserId;
        private string CryptoId;

        private SessionDTO session;
        private readonly FacadeCT _facadeCT;
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();


        public LimitsForm(SessionDTO session, string id)
        {
            LogManager.LoadConfiguration("nlog.config");
            Logger.Info("Limits Form initialized.");
            InitializeComponent();
            this.session = session;
            this.UserId = session.Id;
            this.CryptoId = id;



            // Inicializar label y textbox con la posición inicial
            label1.Text = "Update Limits for " + CryptoId;
            GetLimit();

            // Agregar el evento click para el botón
            button1.Click += new EventHandler(btnUpdateLimits_Click);
        }


        private async void GetLimit()
        {
            try
            {
                // Usa await para esperar el resultado del método asincrónico
                var actualLimit = await _facadeCT.GetLimit(UserId, CryptoId);

                // Convierte el resultado a string y lo asigna al TextBox
                textBox1.Text = actualLimit.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error("An error occurred while obtaining the crypto limits: " + ex.Message);
            }
            finally
            {
                LogManager.Shutdown();
            }
        }


        private async void btnUpdateLimits_Click(object sender, EventArgs e)
        {
            float newLimit = float.Parse(textBox1.Text);
            try
            {
                await UpdateLimit(newLimit);
                MessageBox.Show("Limit updated successfully.");
            }
            catch (Exception ex)
            {
                Logger.Error("Error updating limit: " + ex.Message);
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        private async Task UpdateLimit(float newLimit)
        {
           try
            {
                await _facadeCT.UpdateLimit(newLimit, UserId, CryptoId);
            }
            catch (Exception ex)
            {
                Logger.Error("Error updating limit: " + ex.Message);
            }
            finally
            {
                LogManager.Shutdown();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
