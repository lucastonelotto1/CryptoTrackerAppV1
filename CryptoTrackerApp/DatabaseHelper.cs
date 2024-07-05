using Supabase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoTrackerApp
{
    public class DatabaseHelper
    {
        private Supabase.Client supabaseClient;

        public DatabaseHelper(string url, string key)
        {
            supabaseClient = new Supabase.Client(url, key);
            supabaseClient.InitializeAsync().Wait();
        }

        public async Task<List<CryptoAsset>> GetFavoriteCryptosAsync()
        {
            var response = await supabaseClient.From<FavoriteCrypto>().Select("*").ExecuteAsync();
            return response.Models;
        }

        public async Task AddFavoriteCryptoAsync(CryptoAsset asset)
        {
            var favorite = new FavoriteCrypto
            {
                Id = asset.Id,
                Name = asset.Name,
                Symbol = asset.Symbol,
                PriceUsd = asset.PriceUsd
            };
            await supabaseClient.From<FavoriteCrypto>().Insert(favorite).ExecuteAsync();
        }

        public async Task DeleteFavoriteCryptoAsync(string id)
        {
            await supabaseClient.From<FavoriteCrypto>().Delete().Eq("id", id).ExecuteAsync();
        }
    }

    public class FavoriteCrypto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string PriceUsd { get; set; }
    }
}