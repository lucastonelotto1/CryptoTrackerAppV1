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
using CryptoTrackerApp.Domain;
using System.Threading;
using CryptoTrackerApp.DataAccessLayer.EntityFrameWork.Mapping;
using System.Globalization;

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
        public async Task<List<CryptoDTO>> GetFavoriteCryptosId(string userId)
        {
            var favoriteCryptos = await _repository.Cryptos.GetFavoriteCryptos(userId);
            var favoriteCryptosFromApi =  _cryptoApiClient.GetFavCryptosDTO(favoriteCryptos);

            return favoriteCryptosFromApi;    
        }

        public async Task<List<FavoriteCryptos>> GetFavoriteCryptosFromDb(string userId)
        {
            var favoriteCryptosFromDb = await _repository.Cryptos.GetFavoriteCryptos(userId);
            return favoriteCryptosFromDb;
        }

        // Método para obtener alertas recientes
        public async Task<List<AlertsHistory>> GetRecentAlerts(string userId, DateTime cutoffDate)
        {
            var alerts = await _repository.Alerts.GetRecentAlerts(userId, cutoffDate);
            return alerts;
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
        public async Task AddAlert(string userId, string cryptoIdOutOfLimit, decimal changePercent, string time)
        {
            await _repository.Alerts.AddAlert(userId, cryptoIdOutOfLimit, changePercent, time);
            await _repository.SaveChangesAsync();
        }

        // Método para obtener el precio de una criptomoneda desde la API
        public async Task<CryptoDTO> GetCryptoDetailsAsync(string cryptoId)
        {
            var cryptoDetails = _cryptoApiClient.GetAllCryptosDTO().Find(crypto => crypto.Symbol == cryptoId);
            return cryptoDetails;
        }


        // Método para obtener el historial de una criptomoneda de los últimos 6 meses
        public async Task<List<CryptoAssetHistoryDTO>> GetCryptoHistoryAsync(string cryptoId)
        {
            var history =  _cryptoApiClient.Get6MonthHistoryFrom(cryptoId);
            return history;
        }

        // Método para monitorear los cambios en las criptomonedas
        public async Task MonitorCryptoChangesAsync(string userId, string email, string name)
        {
            var favoriteCryptos = await GetFavoriteCryptosFromDb(userId);

            // Obtener todos los criptoactivos desde la API
            List<CryptoDTO> cryptoAssets = _cryptoApiClient.GetAllCryptosDTO();

            foreach (var favorite in favoriteCryptos)
            {
                var matchingCrypto = cryptoAssets.FirstOrDefault(c => c.Symbol == favorite.CryptoId);
                if (matchingCrypto != null)
                {
                    // Usar decimal directamente sin convertir a double
                    decimal originalChangePercent24Hr = matchingCrypto.ChangePercent24Hr;

                    // Usar el valor absoluto para la comparación con el límite
                    decimal absoluteChangePercent24Hr = Math.Abs(originalChangePercent24Hr);

                    if (absoluteChangePercent24Hr > (decimal)favorite.Limit)
                    {
                        // Enviar el valor formateado en el correo electrónico (F2 = 2 decimales)
                        await _emailService.SendEmailAsync(
                            email,
                            name,
                            $"The cryptocurrency {matchingCrypto.Name} has changed by {originalChangePercent24Hr.ToString("F2", CultureInfo.InvariantCulture)}% in the last 24 hours.",
                            $"<h1>The cryptocurrency {matchingCrypto.Name} has changed by {originalChangePercent24Hr.ToString("F2", CultureInfo.InvariantCulture)}% in the last 24 hours.</h1>"
                        );

                        // Guardar el valor original sin modificar en la alerta
                        string argentinaTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                        // Aquí no necesitas convertir a string con coma, solo guardar el decimal directamente
                        await AddAlert(userId, matchingCrypto.Name, originalChangePercent24Hr, argentinaTime);
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
                //Hasta acá seguro
                if (session != null)
                {
                    // Crear instancia de TaskBackgroundService con las dependencias necesarias
                    var backgroundService = new TaskBackgroundService(_emailService, this);

                    // Inicia la tarea en segundo plano usando la sesión autorizada
                    Task.Run(async () => await backgroundService.RunBackgroundTaskAsync(session));

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
                .Where(asset => !favoriteCryptoIds.Contains(asset.Symbol))
                .ToList();

            return nonFavoriteCryptos;
        }
    }
}