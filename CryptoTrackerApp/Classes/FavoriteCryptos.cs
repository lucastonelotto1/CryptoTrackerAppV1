using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;


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
