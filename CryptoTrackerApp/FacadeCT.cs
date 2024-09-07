using CryptoTrackerApp.DTO;
using CryptoTrackerApp.DataAccessLayer;
using CryptoTrackerApp.Api;
using CryptoTrackerApp.EmailServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoTrackerApp.Infrastructure;
using CryptoTrackerApp.Classes;

namespace CryptoTrackerApp
{
    public class FacadeCT
    {
        private readonly IRepository _repository;
        private readonly ICoinCapApiClient _cryptoApiClient;
        private readonly IEmailService _emailService;

        public FacadeCT(IRepository repository, ICoinCapApiClient cryptoApiClient, IEmailService emailService)
        {
            _repository = repository;
            _cryptoApiClient = cryptoApiClient;
            _emailService = emailService;
        }
        // Método para obtener criptomonedas favoritas
        public async Task<List<CryptoDTO>> GetFavoriteCryptos(string userId)
        {
            var favoriteCryptos = await _repository.Cryptos.GetFavoriteCryptos(userId);
            var favoriteCryptosFromApi =  _cryptoApiClient.GetFavCryptosDTO(favoriteCryptos);  
            MessageBox.Show("Cantidad de Api: " + favoriteCryptosFromApi.Count);
            MessageBox.Show("Cantidad de Db " + favoriteCryptos.Count);
            return favoriteCryptosFromApi;    
        }

        public async Task<List<FavoriteCryptos>> GetFavoriteCryptosFromDb(string userId)
        {
            var favoriteCryptosFromDb = await _repository.Cryptos.GetFavoriteCryptos(userId);
            return favoriteCryptosFromDb;
        }

        // Método para obtener alertas recientes
        public async Task<List<AlertsHistoryDTO>> GetRecentAlerts(string userId, DateTime cutoffDate)
        {
            var alerts = await _repository.Alerts.GetRecentAlerts(userId, cutoffDate);
            var alertDTOs = alerts.Select(alert => new AlertsHistoryDTO(
                alert.CryptoIdOutOfLimit,
                alert.UserId,
                alert.ChangePercent,
                alert.Time
            )).ToList();
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
        public async Task<SessionDTO> Authorize(string email, string password)
        {
            var session = await _repository.Authorize(email, password);
            return new SessionDTO(session.AccessToken, session.User.Id, session.User.Email);
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
            var cryptoDetails = _cryptoApiClient.GetAllCryptosDTO().Find(crypto => crypto.Id == cryptoId);
            return cryptoDetails;
        }

        // Método para obtener el historial de una criptomoneda de los últimos 6 meses
        public async Task<List<CryptoAssetHistoryDTO>> GetCryptoHistoryAsync(string cryptoId)
        {
            var history = _cryptoApiClient.Get6MonthHistoryFrom(cryptoId);
            return history.Select(h => new CryptoAssetHistoryDTO(h.PriceUsd, h.Date)
            {
                VolumeUsd24Hr = h.VolumeUsd24Hr,
                ChangePercent24Hr = h.ChangePercent24Hr
            }).ToList();
        }

        // Método para monitorear cambios de criptomonedas y enviar alertas
        public async Task MonitorCryptoChangesAsync(string userId, string email, string name)
        {
            // Obtener las criptomonedas favoritas del usuario
            var favoriteCryptos = await GetFavoriteCryptosFromDb(userId);  //Todas las criptomonedas favoritas del usuario con la info de la API 

            // Obtener todos los criptoactivos desde la API
            List<CryptoDTO> cryptoAssets = _cryptoApiClient.GetAllCryptosDTO();

            foreach (var favorite in favoriteCryptos)
            {
                var matchingCrypto = cryptoAssets.FirstOrDefault(c => c.Symbol == favorite.CryptoId);
                if (matchingCrypto != null)
                {
                    float changePercent24Hr = Math.Abs((float)matchingCrypto.ChangePercent24Hr);

                    if (changePercent24Hr > favorite.Limit)
                    {
                        // Enviar correo si el cambio de precio excede el límite
                        await _emailService.SendEmailAsync(
                            email,
                            name,
                            $"The cryptocurrency {matchingCrypto.Name} has changed by {matchingCrypto.ChangePercent24Hr}% in the last 24 hours.",
                            $"<h1>The cryptocurrency {matchingCrypto.Name} has changed by {matchingCrypto.ChangePercent24Hr}% in the last 24 hours.</h1>"
                        );

                        // Guardar la alerta en el repositorio
                        string argentinaTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                        await AddAlert(userId, matchingCrypto.Name, changePercent24Hr, argentinaTime);
                    }
                }
            }
        }

        // Método para autorizar y ejecutar la tarea en segundo plano
        public async Task<SessionDTO> AuthorizeAndStartBackgroundTask(string email, string password)
        {
            try
            {
                var session = await Authorize(email, password);

                if (session != null && session.AccessToken != null)
                {
                    // Crear instancia de TaskBackgroundService con las dependencias necesarias
                    var backgroundService = new TaskBackgroundService(_emailService, this);

                    // Inicia la tarea en segundo plano usando la sesión autorizada
                    await backgroundService.RunBackgroundTaskAsync(session);
                }

                return session;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during authorization: " + ex.Message, ex);
            }
        }

        // Método para obtener las criptomonedas no favoritas
        public async Task<List<CryptoDTO>> GetNonFavoriteCryptos(string userId)
        {
            var assets = _cryptoApiClient.GetAllCryptosDTO();
            var favoriteCryptos = await _repository.Cryptos.GetFavoriteCryptos(userId);
            var favoriteCryptoIds = favoriteCryptos.Select(fc => fc.CryptoId).ToList();

            var nonFavoriteCryptos = assets
                .Where(asset => !favoriteCryptoIds.Contains(asset.Id))
                .ToList();

            return nonFavoriteCryptos;
        }
    }
}