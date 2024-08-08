using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CryptoTrackerApp.Classes;

public class CoinCapApiClient
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<List<CryptoAsset>> GetCryptoAssetsAsync()
    {
        string url = "https://api.coincap.io/v2/assets";
        string json = await client.GetStringAsync(url);
        dynamic data = JsonConvert.DeserializeObject(json);

        List<CryptoAsset> assets = new List<CryptoAsset>();
        foreach (var item in data.data)
        {
            assets.Add(new CryptoAsset
            {
                Id = item.id,
                Rank = item.rank,
                Symbol = item.symbol,
                Name = item.name,
                Supply = item.supply,
                MaxSupply = item.maxSupply,
                MarketCapUsd = item.marketCapUsd,
                VolumeUsd24Hr = item.volumeUsd24Hr,
                PriceUsd = item.priceUsd,
                ChangePercent24Hr = item.changePercent24Hr,
                Vwap24Hr = item.vwap24Hr,
                Explorer = item.explorer
            });
        }
        return assets;
    }

    public async Task<List<CryptoPriceHistory>> GetCryptoAssetHistoryAsync(string cryptoId)
    {
        var url = $"https://api.coincap.io/v2/assets/{cryptoId}/history?interval=d1";
        using (var response = await client.GetAsync(url))
        {
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<CoinCapHistoryResponse>(jsonResponse);
                return apiResponse.Data;
            }
            else
            {
                throw new HttpRequestException($"Error getting history data: {response.StatusCode}");
            }
        }
    }

    public class CoinCapHistoryResponse
    {
        [JsonProperty("data")]
        public List<CryptoPriceHistory> Data { get; set; }
    }


    public async Task<CryptoAsset> GetCryptoAssetByIdAsync(string cryptoId)
    {
        string url = $"https://api.coincap.io/v2/assets/{cryptoId}";
        string json = await client.GetStringAsync(url);
        dynamic data = JsonConvert.DeserializeObject(json);

        var item = data.data;
        CryptoAsset asset = new CryptoAsset
        {
            Id = item.id,
            Rank = item.rank,
            Symbol = item.symbol,
            Name = item.name,
            Supply = item.supply,
            MaxSupply = item.maxSupply,
            MarketCapUsd = item.marketCapUsd,
            VolumeUsd24Hr = item.volumeUsd24Hr,
            PriceUsd = item.priceUsd,
            ChangePercent24Hr = item.changePercent24Hr,
            Vwap24Hr = item.vwap24Hr,
            Explorer = item.explorer
        };

        return asset;
    }

}