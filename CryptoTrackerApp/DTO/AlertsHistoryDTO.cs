namespace CryptoTrackerApp.DTO
{
    public class AlertsHistoryDTO
    {
        public string CryptoId { get; set; }
        public string UserId { get; set; }
        public float ChangePercent { get; set; }
        public string AlertTime { get; set; }

        public AlertsHistoryDTO(string cryptoId, string userId, float changePercent, string alertTime)
        {
            CryptoId = cryptoId;
            UserId = userId;
            ChangePercent = changePercent;
            AlertTime = alertTime;
        }
    }
}