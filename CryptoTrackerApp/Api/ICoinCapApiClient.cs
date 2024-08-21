using CryptoTrackerApp.Domain;
using CryptoTrackerApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrackerApp.Api
{
    public interface ICoinCapApiClient
    {
        List<CryptoDTO> GetFavCryptosDTO(List<string> pList);

        List<CryptoDTO> GetAllCryptosDTO();

        List<HistoryItem> Get6MonthHistoryFrom(string crypto);
    }
}
