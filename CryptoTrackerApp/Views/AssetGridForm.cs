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
        public AssetGridForm(Session session)
        {
            InitializeComponent();
            userId = session.User.Id;
            apiClient = new CoinCapApiClient();
            LoadDataAsync();
            // Configura el cliente de Supabase
            string url = "https://cjulheqhpurkozgepnja.supabase.co";
            string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImNqdWxoZXFocHVya296Z2VwbmphIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTcxOTk2MTA5MiwiZXhwIjoyMDM1NTM3MDkyfQ.K_Xbt0gItJ9U3NFFYlKk-_n-a98GNsFVB4BwCymRbck";
            supabaseClient = new Supabase.Client(url, key);
            supabaseClient.InitializeAsync().Wait();
        }

        private async void LoadDataAsync()
        {
            List<CryptoAsset> assets = await apiClient.GetCryptoAssetsAsync();
            List<string> cryptoIds = new List<string>();

            foreach (var asset in assets)
            {
                cryptoIds.Add(asset.Symbol);
            }

            string json = JsonConvert.SerializeObject(cryptoIds);
            //MessageBox.Show("Lista de IDs de criptomonedas: " + json);

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
                    idCryptoArray = favoriteCryptos.SelectMany(x => x.IdCrypto).ToArray();
                    string favoriteJson = JsonConvert.SerializeObject(idCryptoArray);
                    //MessageBox.Show("Se encontraron las cryptos, todo legal. JSON: " + favoriteJson);
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
            string nonFavoriteJson = JsonConvert.SerializeObject(nonFavoriteIds);
            //   MessageBox.Show("Lista de IDs de criptomonedas no favoritas: " + nonFavoriteJson);

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
                else
                {
                    //     MessageBox.Show($"Cripto favorita, no se agrega: {asset.Name}");
                }
            }
        }

        private async void btnAddCrypto_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                string selectedCryptoId = selectedRow.Cells["symbol"].Value.ToString().ToUpper(); // Asegúrate de que el nombre de la columna "symbol" coincide

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

                        if (!idCryptoList.Contains(selectedCryptoId))
                        {
                            idCryptoList.Add(selectedCryptoId);
                            umbralList.Add(15); // Añade el valor 15 a la lista Umbral

                            favoriteCryptos.IdCrypto = idCryptoList.ToArray();
                            favoriteCryptos.Umbral = umbralList.ToArray(); // Actualiza la lista Umbral

                            var updateResponse = await supabaseClient
                                .From<FavoriteCryptos>()
                                .Update(favoriteCryptos);

                            if (updateResponse != null)
                            {
                                MessageBox.Show("Crypto added to favorites successfully.");

                                // Limpia el DataGridView antes de recargar los datos
                                dataGridView1.Rows.Clear();

                                // Recarga los datos
                                LoadDataAsync();
                            }
                            else
                            {
                                MessageBox.Show("Failed to update favorite cryptos.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Selected crypto is already in favorites.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No favorite cryptos found for this user.");
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



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AssetGridForm_Load(object sender, EventArgs e)
        {

        }

        private void AddFavorite_Click_1(object sender, EventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            InitializeComponent();
            MainForm mainForm = new MainForm(session);
            mainForm.Show();
            this.Hide();
        }

        private void AssetGridForm_Load_1(object sender, EventArgs e)
        {

        }



        /*private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AssetGrid_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void removeFavorite_Click(object sender, EventArgs e)
        {

        }*/
    }
}