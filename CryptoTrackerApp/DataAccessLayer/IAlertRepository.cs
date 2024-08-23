using CryptoTrackerApp.Domain;

public interface IAlertRepository
{
    Task<List<Alert>> GetRecentAlerts(Guid userId, DateTime cutoffDate);
    Task AddAlert(Alert alert);
}
