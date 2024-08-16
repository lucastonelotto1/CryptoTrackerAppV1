using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CryptoTrackerApp.Classes;
using Supabase.Gotrue;
using System.Security.Policy;
using System.Globalization;
using Supabase.Postgrest.Models;
using NLog.LayoutRenderers;
using Microsoft.VisualBasic.ApplicationServices;

public class DatabaseHelper : BaseModel
{
    private readonly Supabase.Client supabaseClient;
    private readonly string url;
    private readonly string key;
    public DatabaseHelper()
    {
        url = "https://cjulheqhpurkozgepnja.supabase.co";
        key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImNqdWxoZXFocHVya296Z2VwbmphIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTcxOTk2MTA5MiwiZXhwIjoyMDM1NTM3MDkyfQ.K_Xbt0gItJ9U3NFFYlKk-_n-a98GNsFVB4BwCymRbck";
        supabaseClient = new Supabase.Client(url, key);
        supabaseClient.InitializeAsync().Wait();
    }

    public async Task<List<FavoriteCryptos>> GetFavoriteCryptos(string userId)
    {
        Guid userIdGuid;
        if (!Guid.TryParse(userId, out userIdGuid))
        {
            throw new Exception("Invalid user ID format.");
        }

        var response = await supabaseClient
            .From<FavoriteCryptos>()
            .Where(x => x.UserId == userIdGuid)
            .Get();

        return response.Models;
    }
    public async Task<List<AlertsHistory>> GetRecentAlerts(string userId, DateTime cutoffDate)
    {
        // Definir la lista para almacenar las alertas recientes
        List<AlertsHistory> recentAlerts = new List<AlertsHistory>();

        // Convertir el userId de string a Guid
        Guid userIdGuid;
        if (!Guid.TryParse(userId, out userIdGuid))
        {
            throw new Exception("Invalid user ID format.");
        }

        // Obtener las alertas del usuario desde la base de datos
        var response = await supabaseClient
            .From<AlertsHistory>()
            .Where(x => x.UserId == userIdGuid)
            .Get();

        // Recorrer las alertas obtenidas
        foreach (var alert in response.Models)
        {
            DateTime alertTime;
            // Convertir alert.Time de string a DateTime usando el formato especificado
            if (DateTime.TryParseExact(alert.Time, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out alertTime))
            {
                // Verificar si la alerta es reciente comparando las fechas
                if (alertTime >= cutoffDate)
                {
                    // Agregar la alerta a la lista de alertas recientes
                    recentAlerts.Add(alert);
                    //MessageBox.Show("Alert", alert.Time);
                }
            }
            else
            {
                MessageBox.Show($"Failed to parse alert time: {alert.Time}");
            }
        }

        return recentAlerts;
    }

    public async Task AddFavoriteCrypto(string userId, string cryptoId)
    {
        Guid userIdGuid;
        if (!Guid.TryParse(userId, out userIdGuid))
        {
            throw new Exception("Invalid user ID format.");
        }

        var favoriteCrypto = new FavoriteCryptos
        {
            UserId = userIdGuid,
            CryptoId = cryptoId
        };

        await supabaseClient
            .From<FavoriteCryptos>()
            .Insert(favoriteCrypto);
    }

    public async Task RemoveFavoriteCrypto(string userId, string cryptoId)
    {
        Guid userIdGuid;
        if (!Guid.TryParse(userId, out userIdGuid))
        {
            throw new Exception("Invalid user ID format.");
        }

        var response = await supabaseClient
            .From<FavoriteCryptos>()
            .Where(x => x.UserId == userIdGuid && x.CryptoId == cryptoId)
            .Get();

        var favoriteCrypto = response.Models.FirstOrDefault();

        if (favoriteCrypto != null)
        {
            await supabaseClient
                .From<FavoriteCryptos>()
                .Delete(favoriteCrypto);
        }
    }

    public async Task <Session> Authorize(string email, string password)
    {
        return await supabaseClient.Auth.SignIn(email, password);
    }

    public async Task <float> GetLimitDb(string UserId, string cryptoId) 
    {
        Guid userIdGuid;
        if (!Guid.TryParse(UserId, out userIdGuid))
        {
            throw new Exception("Invalid user ID format.");
        }

        var response = await supabaseClient
            .From<FavoriteCryptos>()
            .Where(x => x.UserId == userIdGuid && x.CryptoId == cryptoId)
            .Select("Limit")
            .Single();

        return response.Limit;
    }

    public async Task UpdateLimitDb(float newLimit, string UserId ,string cryptoId)
    {
        var updates = new { Limit = newLimit };
        Guid userIdGuid;

        if (!Guid.TryParse(UserId, out userIdGuid))
        {
            throw new Exception("Invalid user ID format.");
        }
        var response = await supabaseClient
                .From<FavoriteCryptos>()
                .Where(x => x.UserId == userIdGuid && x.CryptoId == cryptoId)
                .Set(x => x.Limit, newLimit)
                .Update();

    }

}

