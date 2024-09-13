using Newtonsoft.Json;
using CryptoTrackerApp.Classes;
using CryptoTrackerApp.Api;
using CryptoTrackerApp.Domain;
using CryptoTrackerApp.DTO;
using CryptoTrackerApp.DataAccessLayer.EntityFrameWork.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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

                if (item.CryptoId == responseItem.symbol.ToString())
                {
                    CryptoDTO objectDTO = CryptoMapper.MapToDTO(responseItem);
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

    public List<CryptoAssetHistoryDTO> Get6MonthHistoryFrom(string cryptoId)
    {
        var localNow = DateTime.Now;
        var sixMonthsBack = ((DateTimeOffset)(localNow.AddMonths(-6).ToUniversalTime())).ToUnixTimeMilliseconds();
        var historyConnection = new ApiResponse();

        // Construir la URL correctamente
        string historyUrl = $"https://api.coincap.io/v2/assets/{cryptoId}/history?interval=d1";
        
        
        historyConnection.GetAPIResponseItem(historyUrl);


        var historyData =  historyConnection.Data;

        // Convertir dynamic a List<CryptoAssetHistoryDTO>
        List<CryptoAssetHistoryDTO> historyList = CryptoMapper.MapToCryptoAssetHistoryDTO(historyData);


        return historyList.Where(h => ((DateTimeOffset)h.Date).ToUnixTimeMilliseconds() >= sixMonthsBack).ToList();
    }

}