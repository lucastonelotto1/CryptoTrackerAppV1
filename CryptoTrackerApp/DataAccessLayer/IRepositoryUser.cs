using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoTrackerApp.Domain;

namespace CryptoTrackerApp.DataAccessLayer
{
    public interface IRepositoryUser : IRepository<User>
    {
    }
}
