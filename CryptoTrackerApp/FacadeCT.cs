using CryptoTrackerApp.DTO;
using CryptoTrackerApp.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoTrackerApp.Api;
using System.Diagnostics.CodeAnalysis;


namespace CryptoTrackerApp
{
    public class FacadeCT
    {
        private readonly IRepository _repository;
        private readonly ICoinCapApiClient _cryptoApiClient;

        public FacadeCT(IRepository repository, ICoinCapApiClient cryptoApiClient)
        {
            _repository = repository;
            _cryptoApiClient = cryptoApiClient;
        }

        // Método para obtener criptomonedas favoritas
        public async Task<List<CryptoDTO>> GetFavoriteCryptos(string userId)
        {
            // Obtener las criptomonedas favoritas del usuario desde el repositorio
            var favoriteCryptosIds = await _repository.Cryptos.GetFavoriteCryptos(userId);

            // Utilizar el cliente API para obtener los detalles de las criptomonedas favoritas
            var favoriteCryptos = _cryptoApiClient.GetFavCryptosDTO(favoriteCryptosIds);

            return favoriteCryptos;
        }

        // Método para obtener alertas recientes
        public async Task<List<AlertsHistoryDTO>> GetRecentAlerts(string userId, DateTime cutoffDate)
        {
            var alerts = await _repository.Alerts.GetRecentAlerts(userId, cutoffDate);
            var alertDTOs = new List<AlertsHistoryDTO>();

            foreach (var alert in alerts)
            {
                alertDTOs.Add(new AlertsHistoryDTO(
                    alert.CryptoIdOutOfLimit,
                    alert.UserId,
                    alert.ChangePercent,
                    alert.Time
                ));
            }
            return alertDTOs;
        }

        // Método para agregar una criptomoneda a favoritas
        public async Task AddFavoriteCrypto(string userId, string cryptoId)
        {
            await _repository.Cryptos.AddFavoriteCrypto(userId, cryptoId);
            await _repository.SaveChangesAsync();
        }

        // Método para eliminar una criptomoneda de favoritas
        public async Task RemoveFavoriteCrypto(string userId, string cryptoId)
        {
            await _repository.Cryptos.RemoveFavoriteCrypto(userId, cryptoId);
            await _repository.SaveChangesAsync();
        }

        // Método para autorizar a un usuario
        public async Task<SessionDTO> AuthorizeAsync(string email, string password)
        {
            var session = await _repository.Authorize(email, password);
            return new SessionDTO(session.AccessToken, session.User.Id);
        }

        // Método para obtener el límite de una criptomoneda
        public async Task<float> GetLimit(string userId, string cryptoId)
        {
            return await _repository.Cryptos.GetLimit(userId, cryptoId);
        }

        // Método para actualizar el límite de una criptomoneda
        public async Task UpdateLimit(float newLimit, string userId, string cryptoId)
        {
            await _repository.Cryptos.UpdateLimit(userId, cryptoId, newLimit);
            await _repository.SaveChangesAsync();
        }

        // Método para agregar una alerta
        public async Task AddAlert(string userId, string cryptoIdOutOfLimit, float changePercent, string time)
        {
            await _repository.Alerts.AddAlert(userId, cryptoIdOutOfLimit, changePercent, time);
            await _repository.SaveChangesAsync();
        }

        // Método para obtener el precio de una criptomoneda desde la API
        public async Task<CryptoDTO> GetCryptoDetailsAsync(string cryptoId)
        {
            var cryptoDetails = _cryptoApiClient.GetAllCryptosDTO()
                                                .Find(crypto => crypto.Id == cryptoId);
            return cryptoDetails;
        }

        // Método para obtener el historial de una criptomoneda de los últimos 6 meses
        public async Task<List<CryptoAssetHistoryDTO>> GetCryptoHistoryAsync(string cryptoId)
        {
            var history = _cryptoApiClient.Get6MonthHistoryFrom(cryptoId);
            return history.Select(h => new CryptoAssetHistoryDTO(h.PriceUsd, h.Date)
            {
                // Si necesitas inicializar otras propiedades que no están en el constructor, lo haces aquí
                VolumeUsd24Hr = h.VolumeUsd24Hr,
                ChangePercent24Hr = h.ChangePercent24Hr
            }).ToList();
        }
    }
}