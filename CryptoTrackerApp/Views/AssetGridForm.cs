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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using CryptoTrackerApp;

namespace CryptoTrackerApp.Views
{

    public partial class AssetGridForm : Form
    {
        private CoinCapApiClient apiClient;
        private Supabase.Client supabaseClient;
        private string userId = "f3606c6c-072e-4e30-998a-051d73d4153f";
        public AssetGridForm()
        {
            InitializeComponent();
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
                cryptoIds.Add(asset.Id);
            }

            // Mostrar el JSON de los IDs de las criptomonedas (para depuración)
            string json = JsonConvert.SerializeObject(cryptoIds);
            MessageBox.Show("Lista de IDs de criptomonedas: " + json);


            /* foreach (var asset in assets)
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
             }*/

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
                    MessageBox.Show("Se encontraron las cryptos, todo legal. JSON: " + favoriteJson);
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

            // Comparar ambos arrays y obtener los IDs que no estén en el array de favoritos
            List<string> nonFavoriteIds = cryptoIds.Except(idCryptoArray).ToList();

            // Mostrar el JSON de los IDs no favoritos (para depuración)
            string nonFavoriteJson = JsonConvert.SerializeObject(nonFavoriteIds);
            MessageBox.Show("Lista de IDs de criptomonedas no favoritas: " + nonFavoriteJson);

            // Agregar las criptomonedas no favoritas al DataGridView
            foreach (var asset in assets)
            {
                if (nonFavoriteIds.Contains(asset.Id))
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
                    MessageBox.Show($"Cripto favorita, no se agrega: {asset.Name}");
                }
            }
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
