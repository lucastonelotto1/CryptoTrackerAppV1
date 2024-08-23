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

        public IAlertRepository Alerts { get; private set; }
        public IUserRepository Users { get; private set; }
        public ICryptoRepository Cryptos { get; private set; }


        public Repository(DatabaseConfig config)
        {
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.txt");
            var databaseConfig = DatabaseConfig.Load(configPath);

            var databaseHelper = new DatabaseHelper(databaseConfig);

            _supabaseClient = new Supabase.Client(config.Url, config.Key);
            _supabaseClient.InitializeAsync().Wait();

            Alerts = new AlertRepository(_supabaseClient);
            Users = new UserRepository(_supabaseClient);
            Cryptos = new CryptoRepository(_supabaseClient);
        }

        public async Task<Session> Authorize(string email, string password)
        {
            return await _supabaseClient.Auth.SignIn(email, password);
        }

        public async Task SaveChangesAsync()
        {
            // En este caso, como estamos usando Supabase, no necesitamos implementar
            // un SaveChanges explícito, ya que las operaciones se ejecutan inmediatamente.
            // Sin embargo, si en el futuro cambias a otra tecnología de base de datos,
            // podrías implementar aquí la lógica para guardar todos los cambios pendientes.
            await Task.CompletedTask;
        }
    }
}
