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

    public async Task<List<Alert>> GetRecentAlerts(string userId, DateTime cutoffDate)
    {
        var response = await _supabaseClient
            .From<Alert>()
            .Where(x => x.UserId == userId)
            .Get();

        return response.Models
            .Where(alert => DateTime.ParseExact(alert.Time, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture) >= cutoffDate)
            .ToList();
    }

    public async Task AddAlert(string userId, string cryptoIdOutOfLimit, float changePercent, string time)
    {
        var alert = new Alert
        {
            UserId = userId,
            CryptoIdOutOfLimit = cryptoIdOutOfLimit,
            ChangePercent = changePercent,
            Time = time
        };
        await _supabaseClient
            .From<Alert>()
            .Insert(alert);
    }
}
