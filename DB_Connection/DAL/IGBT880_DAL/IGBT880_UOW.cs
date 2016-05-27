using IGBT880_DAL.Interfaces;
using IGBT880_DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IGBT880_DAL
{
    public class IGBT880_UOW : IGBT880_IUnitOfWork, IDisposable
    {

        private IDbContext DbContext { get; set; }
        protected IEFRepositoryProvider RepositoryProvider { get; set; }

        public IGBT880_UOW(IEFRepositoryProvider repositoryProvider, IDbContext dbContext)
        {
            DbContext = dbContext;

            repositoryProvider.DbContext = dbContext;
            RepositoryProvider = repositoryProvider;
        }

        // UoW main feature - atomic commit at the end of work
        public void Commit()
        {
            ((DbContext) DbContext).SaveChanges();
        }

        public void RefreshAllEntities()
        {
            foreach (var entity in ((DbContext) DbContext).ChangeTracker.Entries())
            {
                entity.Reload();
            }
        }

        //standard repos
        //public IEFRepository<Results> Results { get {return GetStandardRepo<Results>();}}
              
        // repo with custom methods
        // add it also in EFRepositoryFactories.cs, in method GetCustomFactories
        //public IUserRepository Users => GetRepo<IUserRepository>();
        public IResultsRepository Results { get { return GetRepo<IResultsRepository>(); } }


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

        // try to find repository
        public T GetRepository<T>() where T : class
        {
            var res = GetRepo<T>() ?? GetStandardRepo<T>() as T;
            if (res == null)
            {
                throw new NotImplementedException("No repository for type, " + typeof(T).FullName);
            }
            return res;
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
