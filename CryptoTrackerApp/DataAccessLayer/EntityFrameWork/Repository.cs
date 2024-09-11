using CryptoTrackerApp.Classes;
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
        public ICryptoRepository Cryptos { get; }

        public Repository(Supabase.Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
            Alerts = new AlertRepository();
            Cryptos = new CryptoRepository();
            
        }

        public async Task<Session> Authorize(string email, string password)
        {
            MessageBox.Show("aUTH DE SIGNINWITHPASWW");
            return await _supabaseClient.Auth.SignInWithPassword(email, password);
        }

        public Task SaveChangesAsync()
        {
            // Supabase maneja automáticamente las operaciones, por lo que este método se deja vacío.
            return Task.CompletedTask;
        }

    }
}
