using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrackerApp.Classes
{
    public class CryptoPriceHistory
    {
        [JsonProperty("priceUsd")]
        public string PriceUsd { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }
    }
}
