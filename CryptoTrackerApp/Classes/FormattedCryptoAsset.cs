using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrackerApp.Classes
{
    public class FormattedCryptoAsset
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string FormattedPriceUsd { get; set; }
        public string FormattedChangePercent24Hr { get; set; }
        public string FormattedVolumeUsd24Hr { get; set; }
        public string FormattedVwap24Hr { get; set; }
        public string FormattedMarketCapUsd { get; set; }
    }
}
