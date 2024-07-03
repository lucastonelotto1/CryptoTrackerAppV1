using Supabase;
using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace CryptoTrackerApp
{
    public class Client : BaseModel
    {
        [Column("username")]
        public string username { get; set; }

        [Column("password")]
        public string password { get; set; }

        // Agrega otras propiedades según sea necesario
    }
}
