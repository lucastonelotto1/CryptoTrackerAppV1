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
using CryptoTrackerApp.Classes;
using Newtonsoft.Json;
using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using CryptoTrackerApp;
using Supabase.Gotrue;

namespace CryptoTrackerApp.Views
{

    public partial class AssetGridForm : Form
    {
        private CoinCapApiClient apiClient;
        private Supabase.Client supabaseClient;
        private string userId;
        private Session session;
        private MainForm mainForm;

        public AssetGridForm(Session session, MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            userId = session.User.Id;
            apiClient = new CoinCapApiClient();
            LoadDataAsync();
            string url = "https://cjulheqhpurkozgepnja.supabase.co";
            string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImNqdWxoZXFocHVya296Z2VwbmphIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTcxOTk2MTA5MiwiZXhwIjoyMDM1NTM3MDkyfQ.K_Xbt0gItJ9U3NFFYlKk-_n-a98GNsFVB4BwCymRbck";
            supabaseClient = new Supabase.Client(url, key);
            supabaseClient.InitializeAsync().Wait();
        }

        private async void LoadDataAsync()
        {
            List<CryptoAsset> assets = await apiClient.GetCryptoAssetsAsync();
            List<string> cryptoIds = assets.Select(asset => asset.Symbol).ToList();

            string[] idCryptoArray = new string[0];
            try
            {
                if (!Guid.TryParse(userId, out Guid userIdGuid))
                {
                    MessageBox.Show("Invalid user ID format.");
                    return;
                }

                var response = await supabaseClient
                    .From<FavoriteCryptos>()
                    .Where(x => x.UserId == userIdGuid)
                    .Get();

                var favoriteCryptos = response.Models;

                if (favoriteCryptos != null && favoriteCryptos.Any())
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
                MessageBox.Show("An error occurred while loading crypto assets: " + ex.Message);
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
                    // Verifica si el userId es un GUID válido
                    if (!Guid.TryParse(userId, out Guid userIdGuid))
                    {
                        MessageBox.Show("Invalid user ID format.");
                        return;
                    }

                    // Busca si el usuario ya tiene criptomonedas favoritas en la base de datos
                    var response = await supabaseClient
                        .From<FavoriteCryptos>()
                        .Where(x => x.UserId == userIdGuid)
                        .Get();

                    var favoriteCryptos = response.Models;

                    // Verifica si la criptomoneda seleccionada ya está en los favoritos del usuario
                    if (favoriteCryptos != null && favoriteCryptos.Any(fc => fc.CryptoId == selectedCryptoId))
                    {
                        MessageBox.Show("Selected crypto is already in favorites.");
                        return;
                    }

                    // Crea una nueva entrada para la criptomoneda favorita
                    var newFavorite = new FavoriteCryptos
                    {
                        UserId = userIdGuid,
                        CryptoId = selectedCryptoId,
                        Limit = 15 // Valor por defecto
                    };

                    // Inserta la nueva entrada en la base de datos
                    var insertResponse = await supabaseClient
                        .From<FavoriteCryptos>()
                        .Insert(newFavorite);

                    if (insertResponse.Models.Any())
                    {
                        MessageBox.Show("Crypto added to favorites successfully.");
                        // Refresca los datos del DataGridView para reflejar el cambio
                        dataGridView1.Rows.Clear();
                        LoadDataAsync();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add crypto to favorites.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while adding the crypto: " + ex.Message);
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