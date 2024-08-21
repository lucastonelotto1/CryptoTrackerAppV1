using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrackerApp.DataAccessLayer.EntityFrameWork
{
    public abstract class Repository<TDatabaseHelper, TEntity> : IRepository<TEntity> where TEntity : class where TDatabaseHelper : DatabaseHelper
    {
        protected readonly TDatabaseHelper iDatabaseHelper;
        public Repository(TDatabaseHelper pDbContext)
        {
            if (pDbContext == null)
            {
                throw new ArgumentNullException(nameof(pDbContext));
            }

            iDatabaseHelper = pDbContext;
        }

        public void Add(TEntity pEntity)
        {
            if (pEntity == null)
            {
                throw new ArgumentNullException(nameof(pEntity));
            }

            iDatabaseHelper.Set<TEntity>().Add(pEntity);
        }

        public TEntity Get(string pNick)
        {
            return iDatabaseHelper.Set<TEntity>().Find(pNick);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return iDatabaseHelper.Set<TEntity>();
        }
        public void Remove(TEntity pEntity)
        {
            if (pEntity == null)
            {
                throw new ArgumentNullException(nameof(pEntity));
            }

            iDatabaseHelper.Set<TEntity>().Remove(pEntity);
        }



    }
}
