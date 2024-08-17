using Supabase.Gotrue;
using NLog;

namespace CryptoTrackerApp.Views
{
    public partial class LimitsForm : Form
    {
        private string UserId;
        private Session session;
        private string CryptoId;
        private Supabase.Client supabaseClient;
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private DatabaseHelper databaseHelper;


        public LimitsForm(Session session, string id)
        {
            LogManager.LoadConfiguration("nlog.config");
            Logger.Info("Limits Form initialized.");
            InitializeComponent();
            this.session = session;
            this.UserId = session.User.Id;
            this.CryptoId = id;
            databaseHelper = new DatabaseHelper();


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
                var actualLimit = await databaseHelper.GetLimitDb(UserId, CryptoId);

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
                await databaseHelper.UpdateLimitDb(newLimit, UserId, CryptoId);
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
