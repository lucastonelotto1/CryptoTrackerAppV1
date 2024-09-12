using CryptoTrackerApp.Classes;
using Supabase.Gotrue;
using System.Globalization;
using Supabase.Postgrest.Models;
using CryptoTrackerApp.DataAccessLayer.EntityFrameWork;
using CryptoTrackerApp.DataAccessLayer;
using CryptoTrackerApp;
using System.Security.Policy;
using System.Windows.Forms;


public static class DatabaseHelper
{
    private static readonly string url = "https://cjulheqhpurkozgepnja.supabase.co";
    private static readonly string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImNqdWxoZXFocHVya296Z2VwbmphIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTcxOTk2MTA5MiwiZXhwIjoyMDM1NTM3MDkyfQ.K_Xbt0gItJ9U3NFFYlKk-_n-a98GNsFVB4BwCymRbck";

    public static Supabase.Client CreateClient()
    {
        var client = new Supabase.Client(url, key);
        client.InitializeAsync().Wait(); // Inicia el cliente de manera síncrona
        return client;
    }
}


