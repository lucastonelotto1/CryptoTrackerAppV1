namespace CryptoTrackerApp.DTO
{
    public class CryptoDTO
    {
        private string iId;
        private string iRank;
        private string iSymbol;
        private string iName;
        private string iSupply;
        private string iMaxSupply;
        private string iMarketCapUsd;
        private string iVolumeUsd24Hr;
        private decimal iPriceUsd;
        private decimal iChangePercent24Hr;
        private string iVwap24Hr;
        private string iExplore;

        public CryptoDTO(string pId, string pRank, string pSymbol, string pName, string pSupply, string pMaxSupply, string pMarketCapUsd, string pVolumeUsd24Hr, decimal pPriceUsd, decimal pChangePercent24Hr, string pVwap24Hr, string pExplore)
        {
            iId = pId;
            iRank = pRank;
            iSymbol = pSymbol;
            iName = pName;
            iSupply = pSupply;
            iMaxSupply = pMaxSupply;
            iMarketCapUsd = pMarketCapUsd;
            iVolumeUsd24Hr = pVolumeUsd24Hr;
            iPriceUsd = pPriceUsd;
            iChangePercent24Hr = pChangePercent24Hr;
            iVwap24Hr = pVwap24Hr;
            iExplore = pExplore;
        }

        public string Id
        {
            get { return iId; }
            set { iId = value; }
        }
        public string Rank
        {
            get { return iRank; }
            set { iRank = value; }
        }
        public string Symbol
        {
            get { return iSymbol; }
            set { iSymbol = value; }
        }
        public string Name
        {
            get { return iName; }
            set { iName = value; }
        }
        public string Supply
        {
            get { return iSupply; }
            set { iSupply = value; }
        }
        public string MaxSupply
        {
            get { return iMaxSupply; }
            set { iMaxSupply = value; }
        }
        public string MarketCapUsd
        {
            get { return iMarketCapUsd; }
            set { iMarketCapUsd = value; }
        }
        public string VolumeUsd24Hr
        {
            get { return iVolumeUsd24Hr; }
            set { iVolumeUsd24Hr = value; }
        }
        public decimal PriceUsd
        {
            get { return iPriceUsd; }
            set { iPriceUsd = value; }
        }
        public decimal ChangePercent24Hr
        {
            get { return iChangePercent24Hr; }
            set { iChangePercent24Hr = value; }
        }
        public string Vwap24Hr
        {
            get { return iVwap24Hr; }
            set { iVwap24Hr = value; }
        }
        public string Explorer
        {
            get { return iExplore; }
            set { iExplore = value; }
        }
    }
}
