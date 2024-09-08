using CryptoTrackerApp.DTO;
using System;
using System.Collections.Generic;

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
            decimal priceUsd = decimal.Parse(responseItem.priceUsd);
            decimal changePercent24Hr = decimal.Parse(responseItem.changePercent24Hr);
             string vwap24Hr = responseItem.vwap24Hr?.ToString() ?? "N/A";
            string explorer = responseItem.explorer?.ToString() ?? "N/A";

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

            foreach (var responseItem in historyData.data)
            {
                decimal price = decimal.Parse(responseItem.priceUsd);
                long time = responseItem.time;
                DateTimeOffset offset = DateTimeOffset.FromUnixTimeMilliseconds(time);
                DateTime convertedTime = offset.UtcDateTime.ToLocalTime();

                var historyDTO = new CryptoAssetHistoryDTO(price, convertedTime);
                historyList.Add(historyDTO);
            }

            return historyList;
        }
    }
}