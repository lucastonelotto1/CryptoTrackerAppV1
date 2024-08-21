using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrackerApp.Domain
{
    public partial class User
    {
        public string Nickname { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string FavoriteCryptos { get; set; }

        public double Threshold { get; set; }

        public bool ActiveSession { get; set; }

        public User(string nickname, string firstName, string lastName, string password, string email, string favoriteCryptos, double threshold, bool activeSession)
        {
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
            bool exists = false;
            int i = 0;
            foreach (var c in cryptoArray)
            {
                if (c == crypto)
                {
                    exists = true;
                    break;
                }
                i++;
            }
            return exists;
        }
    }
}
