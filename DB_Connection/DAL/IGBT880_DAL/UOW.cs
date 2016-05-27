using DAL_Helper.Interfaces;
using IGBT_DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IGBT_DAL
{
    public class UOW : IUOW, IDisposable
    {
        private IDbContext DbContext { get; set; }
        protected IEFRepositoryProvider RepositoryProvider { get; set; }

        public UOW(IEFRepositoryProvider repositoryProvider, IDbContext dbContext)
        {
            DbContext = dbContext;

            repositoryProvider.DbContext = dbContext;
            RepositoryProvider = repositoryProvider;
        }

        // UoW main feature - atomic commit at the end of work
        public void Commit()
        {
            ((DbContext)DbContext).SaveChanges();
        }

        //standard repos
        public IEFRepository<Results> Results { get { return GetStandardRepo<Results>(); } }

        // repo with custom methods
        // add it also in EFRepositoryFactories.cs, in method GetCustomFactories
        //public ISomeRepo Something { get { return GetRepo<ISomeRepo>(); } }

        // calling standard EF repo provider
        private IEFRepository<T> GetStandardRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>();
        }

        // calling custom repo provier
        private T GetRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (DbContext != null)
            {
                ((DbContext)DbContext).Dispose();
            }
        }

        #endregion

    }
}
