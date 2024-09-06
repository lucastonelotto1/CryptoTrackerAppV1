
using CryptoTrackerApp.Classes;
using System.ComponentModel;
using System.Data;
using CryptoTrackerApp.Views;
using CryptoTracker.Views;
using Supabase.Gotrue;
using NLog;
using CryptoTrackerApp.DTO;
using CryptoTrackerApp.Domain;


namespace CryptoTrackerApp
{
    public partial class MainForm : Form
    {

        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
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
        private DataGridView dataGridViewAlerts;
        private DataGridViewTextBoxColumn AlertHistory;


        // Instances
        private readonly FacadeCT _facadeCT;
        private SessionDTO session;
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private string userId;

        public MainForm(SessionDTO session)
        {
            LogManager.LoadConfiguration("nlog.config");
            Logger.Info("Home Initialized.");
            InitializeComponent();
            userId = session.Id;
            this.session = session;



            // Data Loading
            LoadCryptoAssets();
            LoadAlerts();
        }

        // Designer
        public void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            dataGridViewCryptoAssets = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
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
            dataGridViewAlerts = new DataGridView();
            AlertHistory = new DataGridViewTextBoxColumn();
            ((ISupportInitialize)dataGridViewCryptoAssets).BeginInit();
            ((ISupportInitialize)dataGridViewAlerts).BeginInit();
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
            dataGridViewCryptoAssets.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, dataGridViewTextBoxColumn7, dataGridViewTextBoxColumn8, dataGridViewTextBoxColumn9, Id });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(1, 26, 43);
            dataGridViewCellStyle2.Font = new Font("Sans Serif Collection", 6F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(56, 152, 213);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(1, 26, 43);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridViewCryptoAssets.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCryptoAssets.GridColor = Color.FromArgb(1, 26, 43);
            dataGridViewCryptoAssets.Location = new Point(-2, 0);
            dataGridViewCryptoAssets.Margin = new Padding(3, 7, 3, 3);
            dataGridViewCryptoAssets.MultiSelect = false;
            dataGridViewCryptoAssets.Name = "dataGridViewCryptoAssets";
            dataGridViewCryptoAssets.ReadOnly = true;
            dataGridViewCryptoAssets.RowHeadersVisible = false;
            dataGridViewCryptoAssets.RowTemplate.Height = 35;
            dataGridViewCryptoAssets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCryptoAssets.Size = new Size(1068, 384);
            dataGridViewCryptoAssets.TabIndex = 4;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Ranking";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
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
            btnAddCrypto.Location = new Point(12, 423);
            btnAddCrypto.Name = "btnAddCrypto";
            btnAddCrypto.Size = new Size(146, 44);
            btnAddCrypto.TabIndex = 1;
            btnAddCrypto.Text = "Add Crypto";
            btnAddCrypto.UseVisualStyleBackColor = false;
            btnAddCrypto.Click += btnAddCrypto_Click;
            // 
            // btnViewDetails
            // 
            btnViewDetails.BackColor = Color.FromArgb(64, 228, 175);
            btnViewDetails.Cursor = Cursors.Hand;
            btnViewDetails.Location = new Point(600, 423);
            btnViewDetails.Name = "btnViewDetails";
            btnViewDetails.Size = new Size(146, 44);
            btnViewDetails.TabIndex = 2;
            btnViewDetails.Text = "View Details";
            btnViewDetails.UseVisualStyleBackColor = false;
            btnViewDetails.Click += btnViewDetails_Click;
            // 
            // btnRemoveCrypto
            // 
            btnRemoveCrypto.BackColor = Color.FromArgb(64, 228, 175);
            btnRemoveCrypto.Cursor = Cursors.Hand;
            btnRemoveCrypto.Location = new Point(920, 423);
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
            btnLimits.Location = new Point(288, 423);
            btnLimits.Name = "btnLimits";
            btnLimits.Size = new Size(146, 44);
            btnLimits.TabIndex = 5;
            btnLimits.Text = "Boundaries";
            btnLimits.UseVisualStyleBackColor = false;
            btnLimits.Click += btnLimits_Click;
            // 
            // dataGridViewAlerts
            // 
            dataGridViewAlerts.AllowUserToAddRows = false;
            dataGridViewAlerts.AllowUserToResizeRows = false;
            dataGridViewAlerts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewAlerts.BackgroundColor = Color.FromArgb(0, 18, 30);
            dataGridViewAlerts.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(1, 29, 43);
            dataGridViewCellStyle3.Font = new Font("Sans Serif Collection", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(1, 29, 43);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dataGridViewAlerts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewAlerts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewAlerts.Columns.AddRange(new DataGridViewColumn[] { AlertHistory });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(1, 26, 43);
            dataGridViewCellStyle4.Font = new Font("Sans Serif Collection", 6F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(56, 152, 213);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(1, 26, 43);
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dataGridViewAlerts.DefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewAlerts.GridColor = Color.FromArgb(1, 26, 43);
            dataGridViewAlerts.Location = new Point(1063, 0);
            dataGridViewAlerts.MultiSelect = false;
            dataGridViewAlerts.Name = "dataGridViewAlerts";
            dataGridViewAlerts.ReadOnly = true;
            dataGridViewAlerts.RowHeadersVisible = false;
            dataGridViewAlerts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewAlerts.Size = new Size(257, 384);
            dataGridViewAlerts.TabIndex = 6;
            // 
            // AlertHistory
            // 
            AlertHistory.HeaderText = "Notifications";
            AlertHistory.Name = "AlertHistory";
            AlertHistory.ReadOnly = true;
            AlertHistory.ToolTipText = "Notifications";
            // 
            // MainForm
            // 
            BackColor = Color.FromArgb(0, 18, 30);
            ClientSize = new Size(1319, 479);
            Controls.Add(dataGridViewAlerts);
            Controls.Add(btnLimits);
            Controls.Add(btnRemoveCrypto);
            Controls.Add(btnViewDetails);
            Controls.Add(btnAddCrypto);
            Controls.Add(dataGridViewCryptoAssets);
            Name = "MainForm";
            Text = "Home";
            ((ISupportInitialize)dataGridViewCryptoAssets).EndInit();
            ((ISupportInitialize)dataGridViewAlerts).EndInit();
            ResumeLayout(false);
        }
        private DataGridView dataGridViewCryptoAssets;
        private Button btnViewDetails;
        private Button btnRemoveCrypto;
        private Button btnAddCrypto;


        // Data Load
        private async void LoadCryptoAssets()
        {
            try
            {
                List<CryptoDTO> assets = await _facadeCT.GetFavoriteCryptos(userId);

                dataGridViewCryptoAssets.Rows.Clear(); // Limpia la tabla antes de cargar los nuevos datos

                foreach (var asset in assets)
                {
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
                        formattedVwap24Hr,
                        asset.Id
                    );
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An error occurred while loading crypto assets: " + ex.Message);
                return;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        private async void LoadAlerts()
        {
            try
            {
                DateTime cutoffDate = DateTime.UtcNow.AddDays(-6);
                List<AlertsHistoryDTO> recentAlerts = await _facadeCT.GetRecentAlerts(userId, cutoffDate);

                if (recentAlerts.Any())
                {
                    dataGridViewAlerts.Rows.Clear();
                    foreach (var alert in recentAlerts)
                    {
                        dataGridViewAlerts.Rows.Add($"{alert.CryptoIdOutOfLimit}, has changed {alert.ChangePercent}%, at {alert.Time}");
                    }
                }
                else
                {
                    MessageBox.Show("No alerts found for the last 6 days.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error loading alerts: {ex.Message}");
                return;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }


        // Buttons
        private void btnAddCrypto_Click(object sender, EventArgs e)
        {
            AssetGridForm assetGridForm = new AssetGridForm(session, this);
            assetGridForm.FormClosed += AssetGridForm_FormClosed; // Evento para actualizar los datos al cerrar el formulario
            assetGridForm.Show();
            this.Hide();
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (dataGridViewCryptoAssets.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewCryptoAssets.SelectedRows[0];
                if (selectedRow.Cells["Id"].Value != null) // Verifica que la celda no sea nula
                {
                    string selectedCryptoId = selectedRow.Cells["Id"].Value.ToString();
                    DetailsForm detailsForm = new DetailsForm(selectedCryptoId);
                    detailsForm.Show();
                }
                else
                {
                    MessageBox.Show("The selected crypto asset does not have an ID.");
                }
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
                string selectedCryptoId = selectedRow.Cells["dataGridViewTextBoxColumn2"].Value.ToString();

                try
                {
                    await _facadeCT.RemoveFavoriteCrypto(userId, selectedCryptoId);
                    dataGridViewCryptoAssets.Rows.Clear();
                    LoadCryptoAssets();

                    MessageBox.Show("Crypto successfully removed from favorites.");
                }
                catch (Exception ex)
                {
                    Logger.Error("An error occurred while removing the crypto: " + ex.Message);
                }
                finally
                {
                    LogManager.Shutdown();
                }
            }
            else
            {
                MessageBox.Show("Please select a crypto asset to remove it from favorites.");
            }
        }
        private void btnLimits_Click(object sender, EventArgs e)
        {
            if (dataGridViewCryptoAssets.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewCryptoAssets.SelectedRows[0];
                string selectedCryptoId = selectedRow.Cells["dataGridViewTextBoxColumn2"].Value.ToString();
                LimitsForm changeLimitsForm = new LimitsForm(session,selectedCryptoId);
                changeLimitsForm.Show();
            }
            else
            {
                MessageBox.Show("Please select a crypto asset to update limits.");
            }
        }

        // Tasks
        public async Task UpdateFavoriteCryptos()
        {
            try
            {
                // Usar el Facade para obtener las criptomonedas favoritas
                var favoriteCryptos = await _facadeCT.GetFavoriteCryptos(userId);

                // Limpiar el DataGridView antes de rellenarlo
                dataGridViewCryptoAssets.Rows.Clear();

                // Iterar sobre las criptomonedas favoritas y añadirlas al DataGridView
                foreach (var crypto in favoriteCryptos)
                {
                    dataGridViewCryptoAssets.Rows.Add(
                        crypto.Rank,
                        crypto.Symbol,
                        crypto.Name,
                        crypto.Supply,
                        Math.Round(Convert.ToDecimal(crypto.MarketCapUsd), 2).ToString("F2"),
                        Math.Round(Convert.ToDecimal(crypto.VolumeUsd24Hr), 2).ToString("F2"),
                        Math.Round(crypto.PriceUsd, 2).ToString("F2"),
                        Math.Round(crypto.ChangePercent24Hr, 2).ToString("F3"),
                        Math.Round(Convert.ToDecimal(crypto.Vwap24Hr), 2).ToString("F2"),
                        crypto.Id
                    );
                }

                // Mostrar mensaje si no se encontraron favoritos (opcional)
                if (!favoriteCryptos.Any())
                {
                    MessageBox.Show("No favorite cryptos found for this user.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An error occurred while loading crypto assets: " + ex.Message);
            }
            finally
            {
                LogManager.Shutdown();
            }
        }


        // Data Refresh
        private void AssetGridForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
            UpdateFavoriteCryptos(); // Refresca los datos al volver del AssetGridForm
            //LoadAlerts(); // Refresca las alertas al volver del AssetGridForm
        }
    } }
