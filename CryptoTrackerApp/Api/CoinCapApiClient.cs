using Newtonsoft.Json;
using CryptoTrackerApp.Classes;
using CryptoTrackerApp.Api;
using CryptoTrackerApp.Domain;
using CryptoTrackerApp.DTO;
using CryptoTrackerApp.DataAccessLayer.EntityFrameWork.Mapping;

public class CoinCapApiClient : ICoinCapApiClient
{
    public static string assetsUrl = "https://api.coincap.io/v2/assets";
    public static string history = "https://api.coincap.io/v2/assets/{{id}}/history?interval=d1";
    private dynamic dataAccessor;

    public dynamic DataAccessor
    {
        get
        {
            // Carga los datos si aún no han sido cargados
            if (dataAccessor == null)
            {
                LoadDataFromApi();
            }
            return this.dataAccessor;
        }
        private set { this.dataAccessor = value; }
    }

    public CoinCapApiClient()
    {
        // Constructor vacío, no realiza ninguna solicitud a la API
    }

    public void LoadDataFromApi()
    {
        var response = new ApiResponse();
        response.GetAPIResponseItem(assetsUrl);
        DataAccessor = response.data;
    }

    public List<CryptoDTO> GetFavCryptosDTO(List<string> favoriteList)
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
        foreach (var responseItem in DataAccessor.data) // Se accede a DataAccessor, que cargará los datos si es necesario
        {
            var objectDTO = new CryptoDTO(
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
            list.Add(objectDTO);
        }
        return list;
    }

    public List<HistoryItem> Get6MonthHistoryFrom(string crypto)
    {
        var historyList = new List<HistoryItem>();
        var localNow = DateTime.Now;
        var sixMonthsBack = ((DateTimeOffset)(localNow.AddMonths(-6).ToUniversalTime())).ToUnixTimeMilliseconds();
        var historyConnection = new ApiResponse();
        string historyUrl = string.Format(history, crypto);
        historyConnection.GetAPIResponseItem(historyUrl);
        dynamic HistoryData = historyConnection.Data;
        foreach (var responseItem in HistoryData.data)
        {
            if (responseItem.time >= sixMonthsBack)
            {
                string price = responseItem.priceUsd;

                long time = responseItem.time;
                DateTimeOffset offset = DateTimeOffset.FromUnixTimeMilliseconds(time);
                DateTime convertedTime = offset.UtcDateTime.ToLocalTime();
                var historyItem = new HistoryItem(price, convertedTime);
                historyList.Add(historyItem);
            }
        }
        return historyList;
    }
}