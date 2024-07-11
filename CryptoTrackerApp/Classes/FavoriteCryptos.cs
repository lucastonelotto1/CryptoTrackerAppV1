using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrackerApp.Classes
{
    public class FavoriteCryptos : BaseModel
    {
        [PrimaryKey("idUser", false)]
        public Guid idUser { get; set; }

        [Column("idCrypto")]
        public string[] idCrypto { get; set; }
    }
}
