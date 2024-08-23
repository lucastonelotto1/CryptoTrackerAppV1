using CryptoTrackerApp.Domain;
using System.Globalization;
using CryptoTrackerApp.Domain;

public class AlertRepository : IAlertRepository
{
    private readonly Supabase.Client _supabaseClient;

    public AlertRepository(Supabase.Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }

    public async Task<List<Alert>> GetRecentAlerts(Guid userId, DateTime cutoffDate)
    {
        var response = await _supabaseClient
            .From<Alert>()
            .Where(x => x.UserId == userId)
            .Get();

        return response.Models
            .Where(alert => DateTime.ParseExact(alert.Time, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture) >= cutoffDate)
            .ToList();
    }

    public async Task AddAlert(Alert alert)
    {
        await _supabaseClient
            .From<Alert>()
            .Insert(alert);
    }
}
