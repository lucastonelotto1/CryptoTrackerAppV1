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
        [PrimaryKey("IdUser", false)]
        public Guid IdUser { get; set; }

        [Column("IdCrypto")]
        public string[] IdCrypto { get; set; }

        [Column("Umbral")]
        public float[] Umbral { get; set; }
    }


}
