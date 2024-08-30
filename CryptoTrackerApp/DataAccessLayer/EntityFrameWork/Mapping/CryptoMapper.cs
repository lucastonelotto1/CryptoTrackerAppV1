using CryptoTrackerApp.DTO;
using System;
using System.Collections.Generic;

namespace CryptoTrackerApp.DataAccessLayer.EntityFrameWork.Mapping
{
    public class CryptoMapper
    {
        public static CryptoDTO MapToDTO(dynamic responseItem)
        {
            return new CryptoDTO(
                responseItem.id.ToString(),
                responseItem.rank.ToString(),
                responseItem.symbol.ToString(),
                responseItem.name.ToString(),
                responseItem.supply.ToString(),
                responseItem.maxSupply?.ToString(),
                responseItem.marketCapUsd?.ToString(),
                responseItem.volumeUsd24Hr?.ToString(),
                decimal.Parse(responseItem.priceUsd),
                decimal.Parse(responseItem.changePercent24Hr),
                responseItem.vwap24Hr?.ToString(),
                responseItem.explorer?.ToString()
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