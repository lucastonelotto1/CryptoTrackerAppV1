using CryptoTrackerApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrackerApp.DataAccessLayer.EntityFrameWork
{
    public class RepositoryAlerts : Repository<DatabaseHelper, Alert>, IRepositoryAlerts

    {
        public RepositoryAlerts(DatabaseHelper pDatabaseHelper) : base(pDatabaseHelper)
        {

        }

    }
}
