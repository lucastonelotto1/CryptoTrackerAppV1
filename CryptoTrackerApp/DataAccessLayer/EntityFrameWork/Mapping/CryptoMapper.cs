using CryptoTrackerApp.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CryptoTrackerApp.DataAccessLayer.EntityFrameWork.Mapping
{
    public class CryptoMapper
    {
        public static CryptoDTO MapToDTO(dynamic responseItem)
        {
            string id = responseItem.id?.ToString() ?? "N/A";
            string rank = responseItem.rank?.ToString() ?? "N/A";
            string symbol = responseItem.symbol?.ToString() ?? "N/A";
            string name = responseItem.name?.ToString() ?? "N/A";
            string supply = responseItem.supply?.ToString() ?? "N/A";
            string maxSupply = responseItem.maxSupply?.ToString() ?? "N/A";
            string marketCapUsd = responseItem.marketCapUsd?.ToString() ?? "N/A";
            string volumeUsd24Hr = responseItem.volumeUsd24Hr?.ToString() ?? "N/A";
            string vwap24Hr = responseItem.vwap24Hr?.ToString() ?? "N/A";
            string explorer = responseItem.explorer?.ToString() ?? "N/A";

            decimal priceUsd = 0;
            decimal changePercent24Hr = 0;

            try
            {
                priceUsd = decimal.Parse(responseItem.priceUsd?.ToString() ?? "0");
                changePercent24Hr = decimal.Parse(responseItem.changePercent24Hr?.ToString() ?? "0");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al convertir los valores: {ex.Message}");
            }

            return new CryptoDTO(
                id,
                rank,
                symbol,
                name,
                supply,
                maxSupply,
                marketCapUsd,
                volumeUsd24Hr,
                priceUsd,
                changePercent24Hr,
                vwap24Hr,
                explorer
            );
        }

        public static List<CryptoAssetHistoryDTO> MapToCryptoAssetHistoryDTO(dynamic historyData)
        {
            var historyList = new List<CryptoAssetHistoryDTO>();

            // Utilizar cultura invariante para asegurar el punto como separador decimal
            CultureInfo culture = CultureInfo.InvariantCulture;

            foreach (var responseItem in historyData.data)
            {
                // Intentar convertir el precio de forma segura utilizando cultura invariante
                if (decimal.TryParse(responseItem.priceUsd.ToString(), NumberStyles.Float, culture, out decimal price))
                {
                    // Redondear el precio a 2 decimales
                    price = Math.Round(price, 2);

                    // Convertir el timestamp a DateTime
                    long time = responseItem.time;
                    DateTimeOffset offset = DateTimeOffset.FromUnixTimeMilliseconds(time);
                    DateTime convertedTime = offset.UtcDateTime.ToLocalTime();

                    // Crear y agregar el DTO
                    var historyDTO = new CryptoAssetHistoryDTO(price, convertedTime);
                    historyList.Add(historyDTO);
                }
                else
                {
                    // Si no se puede convertir el precio, manejar el error o ignorar este item
                    MessageBox.Show($"Error al convertir el precio: {responseItem.priceUsd}");
                }
            }

            return historyList;
        }
    }
}