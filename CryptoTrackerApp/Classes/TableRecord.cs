using Supabase.Postgrest.Models;

namespace CryptoTrackerApp.Classes
{
    public class TableRecord : BaseModel
    {
        public List<string> IdCrypto { get; set; }
        public List<int> Umbral { get; set; }
        public string UserEmail { get; set; }
    }
}
