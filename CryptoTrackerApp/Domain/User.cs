using Supabase.Postgrest.Models;
using System;

namespace CryptoTrackerApp.Domain
{
    public partial class User : BaseModel
    {
        // Propiedad Id requerida por Supabase
        public string Id { get; set; }

        public string Nickname { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FavoriteCryptos { get; set; }
        public double Threshold { get; set; }
        public bool ActiveSession { get; set; }

        // Constructor sin parámetros
        public User()
        {
        }

        // Constructor con parámetros
        public User(string nickname, string firstName, string lastName, string password, string email, string favoriteCryptos, double threshold, bool activeSession)
        {
           // Generar un nuevo string para Id
            Nickname = nickname;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Email = email;
            FavoriteCryptos = favoriteCryptos;
            Threshold = threshold;
            ActiveSession = activeSession;
        }

        public bool DoesCryptoExist(string crypto)
        {
            string[] cryptoArray = FavoriteCryptos.Split(' ');
            return cryptoArray.Contains(crypto);
        }
    }
}
