using Supabase.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using CryptoTrackerApp.Classes;
using Supabase.Gotrue;
using NLog;

namespace CryptoTrackerApp.Views
{

    public partial class AssetGridForm : Form
    {
        private CoinCapApiClient apiClient;
        private DatabaseHelper databaseHelper;
        private Session session;
        private MainForm mainForm;
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private string userId;

        


        public AssetGridForm(Session session, MainForm mainForm)
        {
            LogManager.LoadConfiguration("nlog.config");
            Logger.Info("Assets Initialized.");
            this.mainForm = mainForm;
            userId = session.User.Id;

            databaseHelper = new DatabaseHelper();
            apiClient = new CoinCapApiClient();


            InitializeComponent();
            LoadDataAsync();

        }

        private async void LoadDataAsync()
        {
            List<CryptoAsset> assets = await apiClient.GetCryptoAssetsAsync();
            List<string> cryptoIds = assets.Select(asset => asset.Symbol).ToList();

            string[] idCryptoArray = new string[0];
            try
            {

                var favoriteCryptos = await databaseHelper.GetFavoriteCryptos(userId);

                if (favoriteCryptos != null)
                {
                    idCryptoArray = favoriteCryptos.Select(x => x.CryptoId).ToArray();
                }
                else
                {
                    MessageBox.Show("No favorite cryptos found for this user.");
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

            List<string> nonFavoriteIds = cryptoIds.Except(idCryptoArray).ToList();

            foreach (var asset in assets)
            {
                if (nonFavoriteIds.Contains(asset.Symbol))
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

                    var favoriteCryptos = await databaseHelper.GetFavoriteCryptos(userId);

                    // Verifica si la criptomoneda seleccionada ya está en los favoritos del usuario
                    if (favoriteCryptos != null && favoriteCryptos.Any(fc => fc.CryptoId == selectedCryptoId))
                    {
                        MessageBox.Show("Selected crypto is already in favorites.");
                        return;
                    }
                        
                    await databaseHelper.AddFavoriteCrypto(userId, selectedCryptoId);
                    MessageBox.Show("Crypto added to favorites successfully.");
                    // Refresca los datos del DataGridView para reflejar el cambio
                    dataGridView1.Rows.Clear();
                    LoadDataAsync();
                }
                catch (Exception ex)
                {
                    Logger.Error("An error occurred while adding the crypto: " + ex.Message);
                    return;
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