using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Supabase;

/*
namespace CryptoTrackerApp
{
    public partial class MainForm : Form
    {
        private Supabase.Client supabaseClient;
        //private DatabaseHelper databaseHelper;

        public MainForm()
        {
            InitializeComponent();
            // Configura el cliente de Supabase
            string url = "https://your-project-url.supabase.co";
            string key = "your-api-key";
            supabaseClient = new Supabase.Client(url, key);
            supabaseClient.InitializeAsync().Wait();

            //databaseHelper = new DatabaseHelper(url, key);
            //LoadCryptoAssets();

        }

        private void InitializeComponent()
        { 
            //DESIGNER
            dataGridViewCryptoAssets = new DataGridView();
            btnAddCrypto = new Button();
            btnViewDetails = new Button();
            btnSetAlert = new Button();
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
            dataGridViewCryptoAssets.Size = new Size(1177, 317);
            dataGridViewCryptoAssets.TabIndex = 0;

            // 
            // btnAddCrypto
            // 
            btnAddCrypto.Location = new Point(109, 341);
            btnAddCrypto.Name = "btnAddCrypto";
            btnAddCrypto.Size = new Size(94, 29);
            btnAddCrypto.TabIndex = 1;
            btnAddCrypto.Text = "Add Crypto";
            btnAddCrypto.UseVisualStyleBackColor = true;
            btnAddCrypto.Click += btnAddCrypto_Click;
            // 
            // btnViewDetails
            // 
            btnViewDetails.Location = new Point(576, 335);
            btnViewDetails.Name = "btnViewDetails";
            btnViewDetails.Size = new Size(148, 29);
            btnViewDetails.TabIndex = 2;
            btnViewDetails.Text = "View Details";
            btnViewDetails.UseVisualStyleBackColor = true;
            btnViewDetails.Click += btnViewDetails_Click;
            // 
            // btnSetAlert
            // 
            btnSetAlert.Location = new Point(990, 341);
            btnSetAlert.Name = "btnSetAlert";
            btnSetAlert.Size = new Size(94, 29);
            btnSetAlert.TabIndex = 3;
            btnSetAlert.Text = "Set Alert";
            btnSetAlert.UseVisualStyleBackColor = true;
            btnSetAlert.Click += btnSetAlert_Click;
            // 
            // MainForm
            // 
            ClientSize = new Size(1201, 396);
            Controls.Add(btnSetAlert);
            Controls.Add(btnViewDetails);
            Controls.Add(btnAddCrypto);
            Controls.Add(dataGridViewCryptoAssets);
            Name = "MainForm";
            ((System.ComponentModel.ISupportInitialize)dataGridViewCryptoAssets).EndInit();
            ResumeLayout(false);
            //END DESIGNER
        }

        private DataGridView dataGridViewCryptoAssets;
        private Button btnViewDetails;
        private Button btnSetAlert;
        private Button btnAddCrypto;

        // Agrega otros métodos y propiedades según sea necesario

        //METODOS BOTONES
        private async void LoadCryptoAssets()
        {
            try
            {
                var assets = await DatabaseHelper.GetCryptoAssetsAsync();
                dataGridViewCryptoAssets.DataSource = assets;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading crypto assets: " + ex.Message);
            }
        }

        private void btnAddCrypto_Click(object sender, EventArgs e)
        {
            // Lógica para agregar una criptomoneda
            var addCryptoForm = new AddCryptoForm(supabaseClient);
            addCryptoForm.ShowDialog();
            LoadCryptoAssets();
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (dataGridViewCryptoAssets.SelectedRows.Count > 0)
            {
                var selectedAsset = dataGridViewCryptoAssets.SelectedRows[0].DataBoundItem as CryptoAsset;
                var detailsForm = new DetailsForm(selectedAsset);
                detailsForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a crypto asset to view details.");
            }
        }

        private void btnSetAlert_Click(object sender, EventArgs e)
        {
            if (dataGridViewCryptoAssets.SelectedRows.Count > 0)
            {
                var selectedAsset = dataGridViewCryptoAssets.SelectedRows[0].DataBoundItem as CryptoAsset;
                var setAlertForm = new SetAlertForm(selectedAsset);
                setAlertForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a crypto asset to set an alert.");
            }
        }
    }


}
}
*/