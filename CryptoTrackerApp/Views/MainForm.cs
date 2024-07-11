
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Supabase;
using Newtonsoft.Json;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using CryptoTrackerApp.Classes;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;




namespace CryptoTrackerApp
{
    public partial class MainForm : Form
    {
        private Supabase.Client supabaseClient;
        private string userId = "f3606c6c-072e-4e30-998a-051d73d4153f";

        public MainForm()
        {
            InitializeComponent();
            // Configura el cliente de Supabase
            string url = "https://cjulheqhpurkozgepnja.supabase.co";
            string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImNqdWxoZXFocHVya296Z2VwbmphIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTcxOTk2MTA5MiwiZXhwIjoyMDM1NTM3MDkyfQ.K_Xbt0gItJ9U3NFFYlKk-_n-a98GNsFVB4BwCymRbck";
            supabaseClient = new Supabase.Client(url, key);
            supabaseClient.InitializeAsync().Wait();
            LoadCryptoAssets();
        }

        private void InitializeComponent()
        {
            dataGridViewCryptoAssets = new DataGridView();
            btnAddCrypto = new Button();
            btnViewDetails = new Button();
            btnRemoveCrypto = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCryptoAssets).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewCryptoAssets
            // 
            dataGridViewCryptoAssets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCryptoAssets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCryptoAssets.Location = new Point(12, 12);
            dataGridViewCryptoAssets.Name = "dataGridViewCryptoAssets";
            dataGridViewCryptoAssets.RowHeadersWidth = 51;
            dataGridViewCryptoAssets.Size = new Size(1253, 373);
            dataGridViewCryptoAssets.TabIndex = 0;
            // 
            // btnAddCrypto
            // 
            btnAddCrypto.Location = new Point(97, 407);
            btnAddCrypto.Name = "btnAddCrypto";
            btnAddCrypto.Size = new Size(144, 44);
            btnAddCrypto.TabIndex = 1;
            btnAddCrypto.Text = "Add Crypto";
            btnAddCrypto.UseVisualStyleBackColor = true;
            btnAddCrypto.Click += btnAddCrypto_Click;
            // 
            // btnViewDetails
            // 
            btnViewDetails.Location = new Point(552, 407);
            btnViewDetails.Name = "btnViewDetails";
            btnViewDetails.Size = new Size(186, 44);
            btnViewDetails.TabIndex = 2;
            btnViewDetails.Text = "View Details";
            btnViewDetails.UseVisualStyleBackColor = true;
            btnViewDetails.Click += btnViewDetails_Click;
            // 
            // btnRemoveCrypto
            // 
            btnRemoveCrypto.Location = new Point(1047, 407);
            btnRemoveCrypto.Name = "btnRemoveCrypto";
            btnRemoveCrypto.Size = new Size(146, 44);
            btnRemoveCrypto.TabIndex = 3;
            btnRemoveCrypto.Text = "Remove Crypto";
            btnRemoveCrypto.UseVisualStyleBackColor = true;
            btnRemoveCrypto.Click += btnRemoveCrypto_Click;
            // 
            // MainForm
            // 
            ClientSize = new Size(1277, 473);
            Controls.Add(btnRemoveCrypto);
            Controls.Add(btnViewDetails);
            Controls.Add(btnAddCrypto);
            Controls.Add(dataGridViewCryptoAssets);
            Name = "MainForm";
            ((System.ComponentModel.ISupportInitialize)dataGridViewCryptoAssets).EndInit();
            ResumeLayout(false);
        }

        private DataGridView dataGridViewCryptoAssets;
        private Button btnViewDetails;
        private Button btnRemoveCrypto;
        private Button btnAddCrypto;



        private async void LoadCryptoAssets()
        {
            try
            {
                Guid userIdGuid;
                if (!Guid.TryParse(userId, out userIdGuid))
                {
                    MessageBox.Show("Invalid user ID format.");
                    return;
                }

                var response = await supabaseClient
                    .From<FavoriteCryptos>()
                    .Where(x => x.IdUser == userIdGuid)
                    .Get();

                var favoriteCryptos = response.Models;

                if (favoriteCryptos != null && favoriteCryptos.Any())
                {
                    // Transform the results to only get the IdCrypto arrays
                    var idCryptoArray = favoriteCryptos.SelectMany(x => x.IdCrypto).ToArray();

                    // Display the result in the DataGridView (or use it as needed)
                    dataGridViewCryptoAssets.DataSource = idCryptoArray
                        .Select(id => new { IdCrypto = id })
                        .ToList();

                    // Show the JSON representation for debugging
                    string json = JsonConvert.SerializeObject(idCryptoArray);
                    //MessageBox.Show("Se encontraron las cryptos, todo legal. JSON: " + json);
                }
                else
                {
                    MessageBox.Show("No favorite cryptos found for this user.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading crypto assets: " + ex.Message);
            }
        }






        private void btnAddCrypto_Click(object sender, EventArgs e)
        {
            // Lógica para agregar una criptomoneda
            //var addCryptoForm = new AddCryptoForm(supabaseClient);
            //addCryptoForm.ShowDialog();
            LoadCryptoAssets();
        }


        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (dataGridViewCryptoAssets.SelectedRows.Count > 0)
            {
                var selectedAsset = dataGridViewCryptoAssets.SelectedRows[0].DataBoundItem as CryptoAsset;
               // var detailsForm = new DetailsForm(selectedAsset);
               // detailsForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a crypto asset to view details.");
            }
        }

        private void btnRemoveCrypto_Click(object sender, EventArgs e)
        {
            if (dataGridViewCryptoAssets.SelectedRows.Count > 0)
            {
                var selectedAsset = dataGridViewCryptoAssets.SelectedRows[0].DataBoundItem as CryptoAsset;
                //var setAlertForm = new SetAlertForm(selectedAsset);
                //setAlertForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a crypto asset to set an alert.");
            }
        }
       
       
    }


}

