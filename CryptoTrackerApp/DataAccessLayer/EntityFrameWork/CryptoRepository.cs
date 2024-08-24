using CryptoTrackerApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoTrackerApp.Domain;

namespace CryptoTrackerApp.DataAccessLayer.EntityFrameWork
{
    public class CryptoRepository : ICryptoRepository
    {
        private readonly Supabase.Client _supabaseClient;

        public CryptoRepository(Supabase.Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        public async Task<List<FavoriteCryptos>> GetFavoriteCryptos(Guid userId)
        {
            var response = await _supabaseClient
                .From<FavoriteCryptos>()
                .Where(x => x.UserId == userId)
                .Get();

            return response.Models;
        }

        public async Task AddFavoriteCrypto(FavoriteCryptos favoriteCrypto)
        {
            await _supabaseClient
                .From<FavoriteCryptos>()
                .Insert(favoriteCrypto);
        }

        public async Task RemoveFavoriteCrypto(Guid userId, string cryptoId)
        {
            await _supabaseClient
             .From<FavoriteCryptos>()
             .Where(x => x.UserId == userId && x.CryptoId == cryptoId)
             .Delete();

        }

        public async Task<float> GetLimit(Guid userId, string cryptoId)
        {
            var response = await _supabaseClient
                .From<FavoriteCryptos>()
                .Where(x => x.UserId == userId && x.CryptoId == cryptoId)
                .Select("Limit")
                .Single();

            return response.Limit;
        }

        public async Task UpdateLimit(Guid userId, string cryptoId, float newLimit)
        {
            await _supabaseClient
                .From<FavoriteCryptos>()
                .Where(x => x.UserId == userId && x.CryptoId == cryptoId)
                .Set(x => x.Limit, newLimit)
                .Update();
        }
    }
}
