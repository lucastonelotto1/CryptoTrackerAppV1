using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrackerApp.Domain;
    public partial class Alert : BaseModel

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
