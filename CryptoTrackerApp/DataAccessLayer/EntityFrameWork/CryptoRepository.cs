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

        public CryptoRepository(Supabase.Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
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
