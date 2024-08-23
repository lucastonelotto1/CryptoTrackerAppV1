using System.Threading.Tasks;
using CryptoTrackerApp.Domain;

namespace CryptoTrackerApp.DataAccessLayer
{
    public interface IUserRepository
    {
        Task<User> GetUserById(Guid userId);
        Task<User> GetUserByEmail(string email);
        Task AddUser(User user);
        Task UpdateUser(User user);
    }

}
