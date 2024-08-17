using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace CryptoTrackerApp.Classes
{
    public class AlertsHistory : BaseModel
    {
        [PrimaryKey("AlertId")]
        public int Id { get; set; }

        [Column("UserId")]
        public Guid UserId { get; set; }

        [Column("CryptoIdOutOfLimit")]
        public string CryptoIdOutOfLimit { get; set; }

        [Column("ChangePercent")]
        public float ChangePercent { get; set; }

        [Column("Time")]
        public string Time { get; set; }
    }



}
