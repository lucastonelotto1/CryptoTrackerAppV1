using Supabase.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CryptoTrackerApp.Classes;
using Supabase.Gotrue;
using NLog;
using CryptoTrackerApp.DTO;

namespace CryptoTrackerApp.Views
{
    public partial class AssetGridForm : Form
    {
        private readonly FacadeCT _facadeCT;
        private SessionDTO session;
        private MainForm mainForm;
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private string userId;

        public AssetGridForm(FacadeCT facadeCT, SessionDTO session, MainForm mainForm)
        {
            LogManager.LoadConfiguration("nlog.config");
            Logger.Info("Assets Initialized.");
            this.mainForm = mainForm;
            this.userId = session.Id;
            _facadeCT = facadeCT;


            InitializeComponent();
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            try
            {
                // Obtener las criptomonedas no favoritas usando el Facade
                List<CryptoDTO> nonFavoriteCryptos = await _facadeCT.GetNonFavoriteCryptos(userId);

                // Añadir las criptomonedas no favoritas al DataGridView
                foreach (var asset in nonFavoriteCryptos)
                {
                    dataGridView1.Rows.Add(
                        asset.Rank,
                        asset.Symbol,
                        asset.Name,
                        asset.Supply,
                        asset.MaxSupply,
                        asset.MarketCapUsd,
                        asset.VolumeUsd24Hr,
                        asset.PriceUsd,
                        asset.ChangePercent24Hr,
                        asset.Vwap24Hr,
                        asset.Explorer
                    );
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

        private async void btnAddCrypto_Click(object sender, EventArgs e)
        {
            // Verifica que haya una fila seleccionada en el DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtén la fila seleccionada
                var selectedRow = dataGridView1.SelectedRows[0];
                // Obtén el ID de la criptomoneda seleccionada (columna "Symbol")
                string selectedCryptoId = selectedRow.Cells["symbol"].Value.ToString().ToUpper();

                try
                {
                    // Usar el Facade para obtener las criptomonedas favoritas
                    var favoriteCryptos = await _facadeCT.GetFavoriteCryptosId(userId);

                    // Verifica si la criptomoneda seleccionada ya está en los favoritos del usuario
                    if (favoriteCryptos.Any(fc => fc.Id == selectedCryptoId))
                    {
                        MessageBox.Show("Selected crypto is already in favorites.");
                        return;
                    }

                    // Usar el Facade para añadir la criptomoneda a los favoritos
                    await _facadeCT.AddFavoriteCrypto(userId, selectedCryptoId);
                    MessageBox.Show("Crypto added to favorites successfully.");

                    // Refresca los datos del DataGridView para reflejar el cambio
                    dataGridView1.Rows.Clear();
                    LoadDataAsync();
                }
                catch (Exception ex)
                {
                    Logger.Error("An error occurred while adding the crypto: " + ex.Message);
                }
                finally
                {
                    LogManager.Shutdown();
                }
            }
            else
            {
                MessageBox.Show("Please select a crypto asset to add it to favorites.");
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string searchText = searchTextBox.Text.ToLower();
            bool found = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["name"].Value != null && row.Cells["name"].Value.ToString().ToLower().Contains(searchText))
                {
                    row.Visible = true;
                    found = true;
                }
                else
                {
                    row.Visible = false;
                }
            }

            if (!found)
            {
                MessageBox.Show("The Crypto is already in the Favorites List");
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainForm.UpdateFavoriteCryptos();
            mainForm.Show();
        }
    }
}