using System;
using System.Collections.Generic;
using System.Linq;
using CryptoTrackerApp.Domain;
using System.Globalization;
using CryptoTrackerApp.Classes;
using CryptoTrackerApp.DataAccessLayer.EntityFrameWork.Mapping;
using CryptoTrackerApp.DTO;

public class AlertRepository : IAlertRepository
{
    private readonly Supabase.Client _supabaseClient;
    private static readonly string url = "https://cjulheqhpurkozgepnja.supabase.co";
    private static readonly string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImNqdWxoZXFocHVya296Z2VwbmphIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTcxOTk2MTA5MiwiZXhwIjoyMDM1NTM3MDkyfQ.K_Xbt0gItJ9U3NFFYlKk-_n-a98GNsFVB4BwCymRbck";

    public AlertRepository()
    {
        _supabaseClient = new Supabase.Client(url, key);
        _supabaseClient.InitializeAsync().Wait();
    }

    public async Task<List<AlertsHistory>> GetRecentAlerts(string userId, DateTime cutoffDate)
    {
        
        // Obtener todas las alertas de Supabase para el usuario
        var response = await _supabaseClient
            .From<AlertsHistory>()
            .Where(x => x.UserId == userId)
            .Get();

        // Filtrar las alertas según el cutoffDate
        var recentAlerts = response.Models
            .Where(alert => DateTime.ParseExact(alert.Time, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture) >= cutoffDate)
            .ToList();
        
        return recentAlerts;
    }


    public async Task AddAlert(string userId, string cryptoIdOutOfLimit, string changePercent, string time)
    {
        var alert = new AlertsHistory
        {
            UserId = userId,
            CryptoIdOutOfLimit = cryptoIdOutOfLimit,
            ChangePercent = changePercent,
            Time = time
        };
        await _supabaseClient
            .From<AlertsHistory>()
            .Insert(alert);
    }
}
