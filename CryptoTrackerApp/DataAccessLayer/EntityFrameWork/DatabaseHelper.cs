using CryptoTrackerApp.Classes;
using Supabase.Gotrue;
using System.Globalization;
using Supabase.Postgrest.Models;
using CryptoTrackerApp.DataAccessLayer.EntityFrameWork;
using CryptoTrackerApp.DataAccessLayer;
using CryptoTrackerApp;


public static class DatabaseHelper
{
    public static Supabase.Client CreateClient(DatabaseConfig config)
    {
        var client = new Supabase.Client(config.Url, config.Key);
        client.InitializeAsync().Wait(); // Inicia el cliente de manera síncrona
        return client;
    }
}

