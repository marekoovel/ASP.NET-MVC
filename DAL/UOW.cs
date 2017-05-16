using DAL.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
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
        public IEFRepository<Album> Albums { get { return GetStandardRepo<Album>(); } }
        public IEFRepository<Genre> Genres { get { return GetStandardRepo<Genre>(); } }
        public IEFRepository<Artist> Artists { get { return GetStandardRepo<Artist>(); } }
        public IEFRepository<Cart> Carts { get { return GetStandardRepo<Cart>(); } }
        public IEFRepository<OrderDetail> OrderDetails { get { return GetStandardRepo<OrderDetail>(); } }
        public IEFRepository<Order> Orders { get { return GetStandardRepo<Order>(); } }
              
        // repo with custom methods
        // add it also in EFRepositoryFactories.cs, in method GetCustomFactories
        //public IUserRepository Users => GetRepo<IUserRepository>();
        public IGenreRepository Genre { get { return GetRepo<IGenreRepository>(); } }
        public IAlbumRepository Album { get { return GetRepo<IAlbumRepository>(); } }
        public ICartRepository Cart { get { return GetRepo<ICartRepository>(); } }
        public IOrderRepository Order { get { return GetRepo<IOrderRepository>(); } }


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
