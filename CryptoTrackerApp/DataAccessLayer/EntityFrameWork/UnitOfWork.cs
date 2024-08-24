using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoTrackerApp.DataAccessLayer;

namespace CryptoTrackerApp.DataAccessLayer.EntityFrameWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DatabaseHelper iDatabaseHelper;

        private bool iDisposedValue = false;

        public UnitOfWork(DatabaseHelper pDatabaseHelper)
        {
            if (pDatabaseHelper == null)
            {
                throw new NotImplementedException();
            }

            this.iDatabaseHelper = pDatabaseHelper;
            this.UserRepository = new UserRepository(pDatabaseHelper);
        }

        public IUserRepository UserRepository { get; private set; }
        public IAlertRepository AlertRepository { get; private set; }
        public void Complete()
        {
            this.iDatabaseHelper.SaveChanges();
        }
        protected virtual void Dispose(bool pDisposing)
        {
            if (!this.iDisposedValue)
            {
                if (pDisposing)
                {
                    this.iDatabaseHelper.Dispose();
                }
                this.iDisposedValue = true;
            }
        }
        public void Dispose()
        {
            this.Dispose(true);
        }
    }
}
