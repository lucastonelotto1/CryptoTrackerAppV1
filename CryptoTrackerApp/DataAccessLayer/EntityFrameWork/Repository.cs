using Supabase.Gotrue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrackerApp.DataAccessLayer.EntityFrameWork
{
    public class Repository : IRepository
    {
        private readonly Supabase.Client _supabaseClient;

        public IAlertRepository Alerts { get; }
        public IUserRepository Users { get; }
        public ICryptoRepository Cryptos { get; }

        public Repository(Supabase.Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
            Alerts = new AlertRepository(_supabaseClient);
            Users = new UserRepository(_supabaseClient);
            Cryptos = new CryptoRepository(_supabaseClient);
        }

        public async Task<Session> Authorize(string email, string password)
        {
            return await _supabaseClient.Auth.SignIn(email, password);
        }

        public Task SaveChangesAsync()
        {
            // Supabase maneja automáticamente las operaciones, por lo que este método se deja vacío.
            return Task.CompletedTask;
        }
    }
}
