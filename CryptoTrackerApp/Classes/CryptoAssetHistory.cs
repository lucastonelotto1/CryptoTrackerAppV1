using Newtonsoft.Json;


namespace CryptoTrackerApp.Classes
{
    public class CryptoPriceHistory
    {
        [JsonProperty("priceUsd")]
        public decimal PriceUsd { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }
    }
}
