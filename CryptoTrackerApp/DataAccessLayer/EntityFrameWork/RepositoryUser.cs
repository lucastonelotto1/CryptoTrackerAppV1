using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptoTrackerApp.Domain;


namespace CryptoTrackerApp.DataAccessLayer.EntityFrameWork
{
    public class UserRepository : Repository<DatabaseHelper, User>, IRepositoryUser
    {
        public UserRepository(DatabaseHelper DatabaseHelper) : base(DatabaseHelper)
        {
        }

        public User GetCurrentUser()
        {
            var userArray = iDatabaseHelper.Set<User>();
            User currentUser = null;
            foreach (var user in userArray)
            {
                if (user.ActiveSession)
                {
                    currentUser = user;
                }
            }
            if (currentUser == null)
            {
                MessageBox.Show("The user is not logged in");
                LoginForm.log.Error("The user could not be found because they are not logged in");
                throw new ApiException("The user could not be found because they are not logged in");
            }
            else
            {
                return currentUser;
            }
        }
    }

}
