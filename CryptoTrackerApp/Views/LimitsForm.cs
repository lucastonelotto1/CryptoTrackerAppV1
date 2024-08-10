using Supabase.Gotrue;
using Supabase;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptoTrackerApp.Classes;
using System.Collections.Generic;

namespace CryptoTrackerApp.Views
{
    public partial class LimitsForm : Form
    {
        private string UserId;
        private Session session;
        private string CryptoId;
        private Supabase.Client supabaseClient;

        public LimitsForm(Session session, string id)
        {
            InitializeComponent();
            this.session = session;
            this.UserId = session.User.Id;
            this.CryptoId = id;
            string url = "https://cjulheqhpurkozgepnja.supabase.co";
            string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImNqdWxoZXFocHVya296Z2VwbmphIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTcxOTk2MTA5MiwiZXhwIjoyMDM1NTM3MDkyfQ.K_Xbt0gItJ9U3NFFYlKk-_n-a98GNsFVB4BwCymRbck";
            supabaseClient = new Supabase.Client(url, key);
            supabaseClient.InitializeAsync().Wait();

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
                Guid userIdGuid;
                if (!Guid.TryParse(UserId, out userIdGuid))
                {
                    MessageBox.Show("Invalid user ID format.");
                    return;
                }
                
                var response = await supabaseClient
                    .From<FavoriteCryptos>()
                    .Where(x => x.UserId == userIdGuid && x.CryptoId == CryptoId)
                    .Select("Limit")
                    .Single();

                var actualLimit = response.Limit.ToString();

                textBox1.Text = actualLimit;
                return;
            }// Asumiendo que "Limit" es el nombre del campo en la tabla
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while obtaining the crypto limits: " + ex.Message);
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
                MessageBox.Show("Error updating limit: " + ex.Message);
            }
        }

        private async Task UpdateLimit(float newLimit)
        {
            var updates = new { Limit = newLimit };
            Guid userIdGuid;
            if (!Guid.TryParse(UserId, out userIdGuid))
            {
                MessageBox.Show("Invalid user ID format.");
                return;
            }

            var response = await supabaseClient
                .From<FavoriteCryptos>()
                .Where(x => x.UserId == userIdGuid && x.CryptoId == CryptoId)
                .Set(x => x.Limit, newLimit)
                .Update();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
