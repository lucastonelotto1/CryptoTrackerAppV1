public class CryptoAssetHistoryDTO
{
    public decimal PriceUsd { get; set; }
    public DateTime Date { get; set; }

    // Asegúrate de agregar estas propiedades si no existen
    public decimal VolumeUsd24Hr { get; set; }
    public decimal ChangePercent24Hr { get; set; }

    public CryptoAssetHistoryDTO(decimal priceUsd, DateTime date)
    {
        PriceUsd = priceUsd;
        Date = date;
    }
}