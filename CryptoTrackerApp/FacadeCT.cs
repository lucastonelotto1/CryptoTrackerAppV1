
using CryptoTrackerApp.Classes;
using Supabase.Gotrue;

namespace CryptoTrackerApp
{
        public class FacadeCT
        {
            private readonly DatabaseHelper _databaseHelper;
            private readonly CoinCapApiClient _cryptoApiClient;

            public FacadeCT()
            {
                _databaseHelper = new DatabaseHelper();
                _cryptoApiClient = new CoinCapApiClient();
            }

            // Método para obtener criptomonedas favoritas
            public async Task<List<FavoriteCryptos>> GetFavoriteCryptos(string userId)
            {
                return await _databaseHelper.GetFavoriteCryptos(userId);
            }

            // Método para obtener alertas recientes
            public async Task<List<AlertsHistory>> GetRecentAlerts(string userId, DateTime cutoffDate)
            {
                return await _databaseHelper.GetRecentAlerts(userId, cutoffDate);
            }

            // Método para agregar una criptomoneda a favoritas
            public async Task AddFavoriteCrypto(string userId, string cryptoId)
            {
                await _databaseHelper.AddFavoriteCrypto(userId, cryptoId);
            }

            // Método para eliminar una criptomoneda de favoritas
            public async Task RemoveFavoriteCrypto(string userId, string cryptoId)
            {
                await _databaseHelper.RemoveFavoriteCrypto(userId, cryptoId);
            }

            // Método para autorizar a un usuario
            public async Task<Session> Authorize(string email, string password)
            {
                return await _databaseHelper.Authorize(email, password);
            }

            // Método para obtener el límite de una criptomoneda
            public async Task<float> GetLimit(string userId, string cryptoId)
            {
                return await _databaseHelper.GetLimitDb(userId, cryptoId);
            }

            // Método para actualizar el límite de una criptomoneda
            public async Task UpdateLimit(float newLimit, string userId, string cryptoId)
            {
                await _databaseHelper.UpdateLimitDb(newLimit, userId, cryptoId);
            }

            // Método para agregar una alerta
            public async Task AddAlert(string userId, string cryptoIdOutOfLimit, float changePercent, string time)
            {
                await _databaseHelper.AddAlert(userId, cryptoIdOutOfLimit, changePercent, time);
            }

            // Método para interactuar con la API de CoinCap (como ejemplo)
            public async Task<string> GetCryptoPrice(string cryptoId)
            {
                return await _cryptoApiClient.GetCryptoPriceAsync(cryptoId);
            }
        }
    }
