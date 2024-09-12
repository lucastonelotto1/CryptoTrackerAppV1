using CryptoTrackerApp.Classes;
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
        List<CryptoDTO> GetFavCryptosDTO(List<FavoriteCryptos> pList);

        List<CryptoDTO> GetAllCryptosDTO();

        List<CryptoAssetHistoryDTO> Get6MonthHistoryFrom(string crypto);
    }
}
