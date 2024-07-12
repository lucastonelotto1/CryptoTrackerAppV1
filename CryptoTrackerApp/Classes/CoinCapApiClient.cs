using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CryptoTrackerApp;
using Newtonsoft.Json;
using Supabase.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptoTrackerApp.Classes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

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
}