using CryptoTrackerApp.Classes;
using CryptoTrackerApp.DTO;

namespace CryptoTrackerApp.DataAccessLayer.EntityFrameWork.Mapping
{
    internal class AlertHistoryMapper
    {
        // Método para mapear de AlertsHistory a AlertsHistoryDTO
        public static AlertsHistoryDTO MapToDTO(AlertsHistory alertHistory)
        {
            return new AlertsHistoryDTO(
                alertHistory.CryptoIdOutOfLimit,
                alertHistory.UserId,
                alertHistory.ChangePercent,
                alertHistory.Time
            );
        }
    }
}