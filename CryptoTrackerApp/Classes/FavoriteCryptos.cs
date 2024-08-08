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
        [PrimaryKey("FavoriteId")]
        public int Id { get; set; }

        [Column("UserId")]
        public Guid UserId { get; set; }

        [Column("CryptoId")]
        public string CryptoId { get; set; }

        [Column("Limit")]
        public float Limit { get; set; }
    }



}
