using Newtonsoft.Json;
using CryptoTrackerApp.Classes;
using CryptoTrackerApp.Api;
using CryptoTrackerApp.Domain;
using CryptoTrackerApp.DTO;
using CryptoTrackerApp.DataAccessLayer.EntityFrameWork.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

public class CoinCapApiClient : ICoinCapApiClient
{
    public static string assetsUrl = "https://api.coincap.io/v2/assets";
    public static string history = "https://api.coincap.io/v2/assets/{{id}}/history?interval=d1";
    public dynamic dataAccessor;

    public dynamic DataAccessor
    {
        get { return this.dataAccessor; }
        set { this.dataAccessor = value; }
    }

    public CoinCapApiClient()
    {
        var response = new ApiResponse();
        response.GetAPIResponseItem(assetsUrl);
        DataAccessor = response.data;
    }

    public List<CryptoDTO> GetFavCryptosDTO(List<FavoriteCryptos> favoriteList)
    {
        var list = new List<CryptoDTO>();
        foreach (var item in favoriteList)
        {
            foreach (var responseItem in dataAccessor.data)
            {
                if (item == responseItem.id.ToString())
                {
                    var objectDTO = CryptoMapper.MapToDTO(responseItem);
                    list.Add(objectDTO);
                }
            }
        }
        return list;
    }

    public List<CryptoDTO> GetAllCryptosDTO()
    {
        var list = new List<CryptoDTO>();
        foreach (var responseItem in dataAccessor.data)
        {
            var objectDTO = CryptoMapper.MapToDTO(responseItem);
            list.Add(objectDTO);
        }
        return list;
    }

    public List<CryptoAssetHistoryDTO> Get6MonthHistoryFrom(string crypto)
    {
        var localNow = DateTime.Now;
        var sixMonthsBack = ((DateTimeOffset)(localNow.AddMonths(-6).ToUniversalTime())).ToUnixTimeMilliseconds();
        var historyConnection = new ApiResponse();
        string historyUrl = string.Format(history, crypto);
        historyConnection.GetAPIResponseItem(historyUrl);
        var historyData = historyConnection.Data; // Mantener como dynamic aquí si es necesario

        // Convertir dynamic a un tipo fuertemente tipado (por ejemplo, List<CryptoAssetHistoryDTO>)
        List<CryptoAssetHistoryDTO> historyList = CryptoMapper.MapToCryptoAssetHistoryDTO(historyData);

        // Ahora puedes usar LINQ sin problemas
        return historyList.Where(h => ((DateTimeOffset)h.Date).ToUnixTimeMilliseconds() >= sixMonthsBack).ToList();
    }
}