using CryptoTrackerApp.Classes;
using CryptoTrackerApp.Domain;
using CryptoTrackerApp.DTO;

public interface IAlertRepository
{
    Task<List<Alert>> GetRecentAlerts(string userId, DateTime cutoffDate);
    Task AddAlert(string userId, string cryptoIdOutOfLimit, float changePercent, string time);
}
