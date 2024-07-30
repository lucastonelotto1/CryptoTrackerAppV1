
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
using CryptoTrackerApp.Views;
using CryptoTracker.Views;
using Supabase.Gotrue;


namespace CryptoTrackerApp
{
    public partial class MainForm : Form
    {
        private CoinCapApiClient apiClient;
        private EmailService emailService;
        private Supabase.Client supabaseClient;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn Position;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn Id;
        private Button btnLimits;
        private string userId;
        private string email;
        private string username;
        private Session session;

        public MainForm(Session session)
        {
            InitializeComponent();
            userId = session.User.Id;
            email = session.User.Email;
            //username = session.User.UserMetadata["username"].ToString();
            this.session = session;
            apiClient = new CoinCapApiClient();
            emailService = new EmailService();
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
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            Position = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
            Id = new DataGridViewTextBoxColumn();
            btnAddCrypto = new Button();
            btnViewDetails = new Button();
            btnRemoveCrypto = new Button();
            btnLimits = new Button();
            ((ISupportInitialize)dataGridViewCryptoAssets).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewCryptoAssets
            // 
            dataGridViewCryptoAssets.AllowUserToAddRows = false;
            dataGridViewCryptoAssets.AllowUserToResizeRows = false;
            dataGridViewCryptoAssets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCryptoAssets.BackgroundColor = Color.FromArgb(0, 18, 30);
            dataGridViewCryptoAssets.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(1, 29, 43);
            dataGridViewCellStyle1.Font = new Font("Sans Serif Collection", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(1, 29, 43);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridViewCryptoAssets.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCryptoAssets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCryptoAssets.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, Position, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, dataGridViewTextBoxColumn7, dataGridViewTextBoxColumn8, dataGridViewTextBoxColumn9, Id });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(1, 26, 43);
            dataGridViewCellStyle2.Font = new Font("Sans Serif Collection", 6F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(56, 152, 213);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(1, 26, 43);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridViewCryptoAssets.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCryptoAssets.GridColor = Color.FromArgb(1, 26, 43);
            dataGridViewCryptoAssets.Location = new Point(115, 42);
            dataGridViewCryptoAssets.Margin = new Padding(3, 7, 3, 3);
            dataGridViewCryptoAssets.MultiSelect = false;
            dataGridViewCryptoAssets.Name = "dataGridViewCryptoAssets";
            dataGridViewCryptoAssets.ReadOnly = true;
            dataGridViewCryptoAssets.RowHeadersVisible = false;
            dataGridViewCryptoAssets.RowTemplate.Height = 35;
            dataGridViewCryptoAssets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCryptoAssets.Size = new Size(1068, 341);
            dataGridViewCryptoAssets.TabIndex = 4;
            dataGridViewCryptoAssets.CellContentClick += dataGridViewCryptoAssets_CellContentClick;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Ranking";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // Position
            // 
            Position.HeaderText = "Position";
            Position.Name = "Position";
            Position.ReadOnly = true;
            Position.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Symbol";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Name";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Supply";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "Market Cap USD";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.HeaderText = "Volume USD";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.HeaderText = "Price USD";
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewTextBoxColumn8.HeaderText = "Change";
            dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            dataGridViewTextBoxColumn9.HeaderText = "VWAP";
            dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // Id
            // 
            Id.HeaderText = "Id";
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.Visible = false;
            // 
            // btnAddCrypto
            // 
            btnAddCrypto.BackColor = Color.FromArgb(64, 228, 175);
            btnAddCrypto.Cursor = Cursors.Hand;
            btnAddCrypto.Location = new Point(115, 407);
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
            btnViewDetails.Cursor = Cursors.Hand;
            btnViewDetails.Location = new Point(507, 407);
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
            btnRemoveCrypto.Cursor = Cursors.Hand;
            btnRemoveCrypto.Location = new Point(1047, 407);
            btnRemoveCrypto.Name = "btnRemoveCrypto";
            btnRemoveCrypto.Size = new Size(146, 44);
            btnRemoveCrypto.TabIndex = 3;
            btnRemoveCrypto.Text = "Remove Crypto";
            btnRemoveCrypto.UseVisualStyleBackColor = false;
            btnRemoveCrypto.Click += btnRemoveCrypto_Click;
            // 
            // btnLimits
            // 
            btnLimits.BackColor = Color.FromArgb(64, 228, 175);
            btnLimits.Cursor = Cursors.Hand;
            btnLimits.FlatStyle = FlatStyle.Flat;
            btnLimits.Location = new Point(1189, 42);
            btnLimits.Name = "btnLimits";
            btnLimits.Size = new Size(77, 29);
            btnLimits.TabIndex = 5;
            btnLimits.Text = "Boundaries";
            btnLimits.UseVisualStyleBackColor = false;
            btnLimits.Click += new System.EventHandler(btnLimits_Click);
            // 
            // MainForm
            // 
            BackColor = Color.FromArgb(0, 18, 30);
            ClientSize = new Size(1277, 473);
            Controls.Add(btnLimits);
            Controls.Add(btnRemoveCrypto);
            Controls.Add(btnViewDetails);
            Controls.Add(btnAddCrypto);
            Controls.Add(dataGridViewCryptoAssets);
            Name = "MainForm";
            Text = "Home";
            Load += MainForm_Load;
            ((ISupportInitialize)dataGridViewCryptoAssets).EndInit();
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
                cryptoIds.Add(asset.Symbol);
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
                    string favoriteJsonRepresentation = JsonConvert.SerializeObject(idCryptoArray); // Renamed variable
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
            List<string> favoriteIds = cryptoIds.Intersect(idCryptoArray).ToList();

            // Mostrar el JSON de los IDs favoritos (para depuración)
            string favoriteIdsJson = JsonConvert.SerializeObject(favoriteIds);

            // Agregar las criptomonedas favoritas al DataGridView
            for (int i = 0; i < assets.Count; i++)
            {
                var asset = assets[i];
                if (favoriteIds.Contains(asset.Symbol))
                {
                    // Formatear los valores según sea necesario
                    string formattedPriceUsd = Math.Round(asset.PriceUsd, 2).ToString("F2");
                    string formattedChangePercent24Hr = Math.Round(asset.ChangePercent24Hr, 2).ToString("F3");
                    string formattedVolumeUsd24Hr = Math.Round(Convert.ToDecimal(asset.VolumeUsd24Hr), 2).ToString("F2");
                    string formattedVwap24Hr = Math.Round(Convert.ToDecimal(asset.Vwap24Hr), 2).ToString("F2");
                    string formattedMarketCapUsd = Math.Round(Convert.ToDecimal(asset.MarketCapUsd), 2).ToString("F2");

                    dataGridViewCryptoAssets.Rows.Add(
                        asset.Rank,
                        i, // Agregar la posició
                        asset.Symbol,
                        asset.Name,
                        asset.Supply,
                        formattedMarketCapUsd,
                        formattedVolumeUsd24Hr,
                        formattedPriceUsd,
                        formattedChangePercent24Hr,
                        formattedVwap24Hr,
                        asset.Id
                    );
                }
            }
        }




        private void btnAddCrypto_Click(object sender, EventArgs e)
        {
            AssetGridForm assetGridForm = new AssetGridForm(session);
            assetGridForm.Show();
            this.Hide();

        }


        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (dataGridViewCryptoAssets.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewCryptoAssets.SelectedRows[0];
                string selectedCryptoId = selectedRow.Cells["Id"].Value.ToString(); // Asegúrate de que el nombre de la columna "Id" coincide
                DetailsForm detailsForm = new DetailsForm(selectedCryptoId);
                detailsForm.Show();
            }
            else
            {
                MessageBox.Show("Please select a crypto asset to view details.");
            }
        }


        private async void btnRemoveCrypto_Click(object sender, EventArgs e)
        {
            if (dataGridViewCryptoAssets.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewCryptoAssets.SelectedRows[0];
                string selectedCryptoId = selectedRow.Cells["dataGridViewTextBoxColumn2"].Value.ToString(); // Asegúrate de que el nombre de la columna "ID" coincide
                int selectedPosition = Convert.ToInt32(selectedRow.Cells["Position"].Value); // Asegúrate de que el nombre de la columna "Position" coincide

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

                    var favoriteCryptos = response.Models.FirstOrDefault();

                    if (favoriteCryptos != null)
                    {
                        List<string> idCryptoList = favoriteCryptos.IdCrypto.ToList();
                        List<int> umbralList = favoriteCryptos.Umbral.ToList();

                        if (idCryptoList.Contains(selectedCryptoId))
                        {
                            idCryptoList.Remove(selectedCryptoId);
                            if (selectedPosition >= 0 && selectedPosition < umbralList.Count)
                            {
                                umbralList.RemoveAt(selectedPosition); // Eliminar la posición de la lista de umbral
                            }

                            favoriteCryptos.IdCrypto = idCryptoList.ToArray();
                            favoriteCryptos.Umbral = umbralList.ToArray();

                            var updateResponse = await supabaseClient
                                .From<FavoriteCryptos>()
                                .Update(favoriteCryptos);

                            if (updateResponse != null)
                            {
                                // Limpia el DataGridView antes de recargar los datos
                                dataGridViewCryptoAssets.Rows.Clear();
                                LoadCryptoAssets();
                            }
                            else
                            {
                                MessageBox.Show("Failed to update favorite cryptos.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Selected crypto is not in favorites.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No favorite cryptos found for this user.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while removing the crypto: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a crypto asset to remove it from favorites.");
            }
        }




        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridViewCryptoAssets_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void btnLimits_Click(object sender, EventArgs e)
        {
            string toEmail = email; // Correo del destinatario
            string toName = "Client"; // Nombre del destinatario
            string plainTextContent = "Este es el contenido del correo en texto plano.";
            string htmlContent = "<h1>Este es el contenido del correo en HTML.</h1>";

            await emailService.SendEmailAsync(toEmail, toName, plainTextContent, htmlContent);
            MessageBox.Show("Correo de límites enviado.");

        }

    }


}

