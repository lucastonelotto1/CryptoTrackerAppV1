
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
using Supabase.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using CryptoTrackerApp;



namespace CryptoTrackerApp
{
    public partial class MainForm : Form
    {
        private CoinCapApiClient apiClient;
        private Supabase.Client supabaseClient;
        private string userId = "f3606c6c-072e-4e30-998a-051d73d4153f";

        public MainForm()
        {
            InitializeComponent();
            apiClient = new CoinCapApiClient();
            LoadCryptoAssets();
            // Configura el cliente de Supabase
            string url = "https://cjulheqhpurkozgepnja.supabase.co";
            string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImNqdWxoZXFocHVya296Z2VwbmphIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTcxOTk2MTA5MiwiZXhwIjoyMDM1NTM3MDkyfQ.K_Xbt0gItJ9U3NFFYlKk-_n-a98GNsFVB4BwCymRbck";
            supabaseClient = new Supabase.Client(url, key);
            supabaseClient.InitializeAsync().Wait();
            
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dataGridViewCryptoAssets = new DataGridView();
            btnAddCrypto = new Button();
            btnViewDetails = new Button();
            btnRemoveCrypto = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCryptoAssets).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewCryptoAssets
            // 
            // dataGridViewCryptoAssets
            // 
            // dataGridViewCryptoAssets
            // 
            dataGridViewCryptoAssets.AllowUserToAddRows = false;
            dataGridViewCryptoAssets.AllowUserToResizeRows = false; // Evitar que los usuarios ajusten la altura de las filas
            dataGridViewCryptoAssets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCryptoAssets.BackgroundColor = Color.FromArgb(0, 18, 30);
            dataGridViewCryptoAssets.RowHeadersVisible = false; // Ocultar la columna de encabezado de fila

            // Establecer el estilo para las celdas de encabezado de columna
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            columnHeaderStyle.BackColor = Color.FromArgb(1, 29, 43);
            columnHeaderStyle.Font = new Font("Sans Serif Collection", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            columnHeaderStyle.ForeColor = Color.White;
            columnHeaderStyle.SelectionBackColor = Color.FromArgb(1, 29, 43);
            columnHeaderStyle.SelectionForeColor = Color.White;
            columnHeaderStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewCryptoAssets.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            dataGridViewCryptoAssets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // Establecer el estilo para las celdas
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            cellStyle.BackColor = Color.FromArgb(1, 26, 43);
            cellStyle.Font = new Font("Sans Serif Collection", 6F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cellStyle.ForeColor = Color.FromArgb(56, 152, 213);
            cellStyle.SelectionBackColor = Color.FromArgb(1, 26, 43);
            cellStyle.SelectionForeColor = SystemColors.HighlightText;
            cellStyle.WrapMode = DataGridViewTriState.False;
            dataGridViewCryptoAssets.DefaultCellStyle = cellStyle;

            dataGridViewCryptoAssets.GridColor = Color.FromArgb(1, 26, 43);
            dataGridViewCryptoAssets.Location = new Point(115, 42);
            dataGridViewCryptoAssets.Margin = new Padding(3, 7, 3, 3);
            dataGridViewCryptoAssets.MultiSelect = false;
            dataGridViewCryptoAssets.Name = "dataGridViewCryptoAssets";
            dataGridViewCryptoAssets.ReadOnly = true;
            dataGridViewCryptoAssets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCryptoAssets.Size = new Size(1068, 341);
            dataGridViewCryptoAssets.TabIndex = 4;

            // Agregar las columnas
            dataGridViewCryptoAssets.Columns.Add("Rank", "Ranking");
            dataGridViewCryptoAssets.Columns.Add("Symbol", "Symbol");
            dataGridViewCryptoAssets.Columns.Add("Name", "Name");
            dataGridViewCryptoAssets.Columns.Add("Supply", "Supply");
            //dataGridViewCryptoAssets.Columns.Add("MaxSupply", "MaxSupply");
            dataGridViewCryptoAssets.Columns.Add("MarketCapUsd", "Market Cap USD");
            dataGridViewCryptoAssets.Columns.Add("VolumeUsd24Hr", "Volume USD");
            dataGridViewCryptoAssets.Columns.Add("PriceUsd", "Price USD");
            dataGridViewCryptoAssets.Columns.Add("ChangePercent24Hr", "Change");
            dataGridViewCryptoAssets.Columns.Add("Vwap24Hr", "VWAP");

            // Ajustar la altura de las filas y establecer márgenes personalizados para las celdas
            dataGridViewCryptoAssets.RowTemplate.Height = 35; // Aumentar la altura de las filas para incluir el "gap"


            // 
            // btnAddCrypto
            // 
            btnAddCrypto.BackColor = Color.FromArgb(64, 228, 175);
            btnAddCrypto.Location = new Point(97, 407);
            btnAddCrypto.Name = "btnAddCrypto";
            btnAddCrypto.Size = new Size(144, 44);
            btnAddCrypto.TabIndex = 1;
            btnAddCrypto.Text = "Add Crypto";
            btnAddCrypto.UseVisualStyleBackColor = false;
            btnAddCrypto.Click += btnAddCrypto_Click;
            // 
            // btnViewDetails
            // 
            btnViewDetails.BackColor = Color.FromArgb(64, 228, 175);
            btnViewDetails.Location = new Point(552, 407);
            btnViewDetails.Name = "btnViewDetails";
            btnViewDetails.Size = new Size(186, 44);
            btnViewDetails.TabIndex = 2;
            btnViewDetails.Text = "View Details";
            btnViewDetails.UseVisualStyleBackColor = false;
            btnViewDetails.Click += btnViewDetails_Click;
            // 
            // btnRemoveCrypto
            // 
            btnRemoveCrypto.BackColor = Color.FromArgb(64, 228, 175);
            btnRemoveCrypto.Location = new Point(1047, 407);
            btnRemoveCrypto.Name = "btnRemoveCrypto";
            btnRemoveCrypto.Size = new Size(146, 44);
            btnRemoveCrypto.TabIndex = 3;
            btnRemoveCrypto.Text = "Remove Crypto";
            btnRemoveCrypto.UseVisualStyleBackColor = false;
            btnRemoveCrypto.Click += btnRemoveCrypto_Click;
            // 
            // MainForm
            // 
            BackColor = Color.FromArgb(0, 18, 30);
            ClientSize = new Size(1277, 473);
            Controls.Add(btnRemoveCrypto);
            Controls.Add(btnViewDetails);
            Controls.Add(btnAddCrypto);
            Controls.Add(dataGridViewCryptoAssets);
            Name = "MainForm";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewCryptoAssets).EndInit();
            ResumeLayout(false);
        }

        private DataGridView dataGridViewCryptoAssets;
        private Button btnViewDetails;
        private Button btnRemoveCrypto;
        private Button btnAddCrypto;



        private async void LoadCryptoAssets()
        {
            List<CryptoAsset> assets = await apiClient.GetCryptoAssetsAsync();
            List<string> cryptoIds = new List<string>();

            foreach (var asset in assets)
            {
                cryptoIds.Add(asset.Id);
            }

            string json = JsonConvert.SerializeObject(cryptoIds);
            string[] idCryptoArray = new string[0];
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
                    idCryptoArray = favoriteCryptos.SelectMany(x => x.IdCrypto).ToArray();

                    // Show the JSON representation for debugging
                    string favoriteJson = JsonConvert.SerializeObject(idCryptoArray);
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

            // Comparar ambos arrays y obtener los IDs que estén en ambos arrays
            List<string> FavoriteIds = cryptoIds.Intersect(idCryptoArray).ToList();

            // Mostrar el JSON de los IDs favoritos (para depuración)
            string FavoriteJson = JsonConvert.SerializeObject(FavoriteIds);
            //MessageBox.Show("Lista de IDs de criptomonedas favoritas: " + FavoriteJson);

            // Agregar las criptomonedas favoritas al DataGridView
            foreach (var asset in assets)
            {
                if (FavoriteIds.Contains(asset.Id))
                {
                    // Formatear los valores según sea necesario
                    string formattedPriceUsd = Math.Round(asset.PriceUsd, 2).ToString("F2");
                    string formattedChangePercent24Hr = Math.Round(asset.ChangePercent24Hr, 2).ToString("F3");
                    string formattedVolumeUsd24Hr = Math.Round(Convert.ToDecimal(asset.VolumeUsd24Hr), 2).ToString("F2");
                    string formattedVwap24Hr = Math.Round(Convert.ToDecimal(asset.Vwap24Hr), 2).ToString("F2");
                    string formattedMarketCapUsd = Math.Round(Convert.ToDecimal(asset.MarketCapUsd), 2).ToString("F2");

                    dataGridViewCryptoAssets.Rows.Add(
                        asset.Rank,
                        asset.Symbol,
                        asset.Name,
                        asset.Supply,
                        formattedMarketCapUsd,
                        formattedVolumeUsd24Hr,
                        formattedPriceUsd,
                        formattedChangePercent24Hr,
                        formattedVwap24Hr
                    //asset.Explorer
                    );
                }
                else
                {
                    //MessageBox.Show($"Cripto favorita, se agrega: {asset.Name}");
                }
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

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }


}

