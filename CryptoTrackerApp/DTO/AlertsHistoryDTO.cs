namespace CryptoTrackerApp.DTO
{
    public class AlertsHistoryDTO
    {
        public string CryptoIdOutOfLimit { get; set; }
        public string UserId { get; set; }
        public float ChangePercent { get; set; }
        public string Time { get; set; }

        public AlertsHistoryDTO(string cryptoIdOutOfLimit, string userId, float changePercent, string time)
        {
            CryptoIdOutOfLimit = cryptoIdOutOfLimit;
            UserId = userId;
            ChangePercent = changePercent;
            Time = time;
        }
    }
}