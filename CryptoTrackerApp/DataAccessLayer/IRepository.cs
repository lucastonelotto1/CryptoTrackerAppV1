using Supabase.Gotrue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrackerApp.DataAccessLayer
{
    public interface IRepository
    {
        IAlertRepository Alerts { get; }
        IUserRepository Users { get; }
        ICryptoRepository Cryptos { get; }
        Task<Session> Authorize(string email, string password);
        Task SaveChangesAsync();
    }
}