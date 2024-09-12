using CryptoTrackerApp.Classes;
using CryptoTrackerApp.Domain;
using CryptoTrackerApp.DTO;

public interface IAlertRepository
{
    Task<List<AlertsHistory>> GetRecentAlerts(string userId, DateTime cutoffDate);
    Task AddAlert(string userId, string cryptoIdOutOfLimit, decimal changePercent, string time);
}
