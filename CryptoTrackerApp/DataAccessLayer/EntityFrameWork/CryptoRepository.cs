using CryptoTrackerApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoTrackerApp.Domain;
using Supabase.Interfaces;

namespace CryptoTrackerApp.DataAccessLayer.EntityFrameWork
{
    public class CryptoRepository : ICryptoRepository
    {
        private readonly Supabase.Client _supabaseClient;
        private static readonly string url = "https://cjulheqhpurkozgepnja.supabase.co";
        private static readonly string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImNqdWxoZXFocHVya296Z2VwbmphIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTcxOTk2MTA5MiwiZXhwIjoyMDM1NTM3MDkyfQ.K_Xbt0gItJ9U3NFFYlKk-_n-a98GNsFVB4BwCymRbck";



        public CryptoRepository()
        {
            _supabaseClient = new Supabase.Client(url, key);
            _supabaseClient.InitializeAsync().Wait();
        }

        public async Task<List<FavoriteCryptos>> GetFavoriteCryptos(string userId)
        {
            
            var response = await _supabaseClient
                .From<FavoriteCryptos>()
                .Where(x => x.UserId == userId)
                .Get();

            return response.Models;
        }



        public async Task AddFavoriteCrypto(string userId, string favoriteCrypto)
        {
            var ss = new FavoriteCryptos
            {
                UserId = userId,
                CryptoId = favoriteCrypto
            };

            await _supabaseClient
                 .From<FavoriteCryptos>()
                    .Insert(ss);
        }

        public async Task RemoveFavoriteCrypto(string userId, string cryptoId)
        {
            await _supabaseClient
             .From<FavoriteCryptos>()
             .Where(x => x.UserId == userId && x.CryptoId == cryptoId)
             .Delete();

        }

        public async Task<float> GetLimit(string userId, string cryptoId)
        {
            var response = await _supabaseClient
                .From<FavoriteCryptos>()
                .Where(x => x.UserId == userId && x.CryptoId == cryptoId)
                .Select("Limit")
                .Single();

            return response.Limit;
        }

        public async Task UpdateLimit(string userId, string cryptoId, float newLimit)
        {
            await _supabaseClient
                .From<FavoriteCryptos>()
                .Where(x => x.UserId == userId && x.CryptoId == cryptoId)
                .Set(x => x.Limit, newLimit)
                .Update();
        }
    }
}
