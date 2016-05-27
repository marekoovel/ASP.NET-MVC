using System;
using System.Collections.Generic;
using DAL.Interfaces;
using DAL.Repositories;


namespace DAL.Helpers
{
    public class EFRepositoryFactories
    {
        private readonly IDictionary<Type, Func<IDbContext, object>> _repositoryFactories;

        public EFRepositoryFactories()
        {
            _repositoryFactories = GetCustomFactories();
        }

        //this ctor is for testing only, you can give here an arbitrary list of repos
        public EFRepositoryFactories(IDictionary<Type, Func<IDbContext, object>> factories)
        {
            _repositoryFactories = factories;
        }

        //special repos with custom interfaces are registered here
        private static IDictionary<Type, Func<IDbContext, object>> GetCustomFactories()
        {
            return new Dictionary<Type, Func<IDbContext, object>>
            {
                //{typeof(ISomeRepo), dbContext => new SomeRepository(dbContext)},
                 {typeof (IGenreRepository), dbContext => new GenreRepository(dbContext)},
                 {typeof (IAlbumRepository), dbContext => new AlbumRepository(dbContext)},
                  {typeof (ICartRepository), dbContext => new CartRepository(dbContext)},
                   {typeof (IOrderRepository), dbContext => new OrderRepository(dbContext)},

            };
        }

        public Func<IDbContext, object> GetRepositoryFactory<T>()
        {

            Func<IDbContext, object> factory;
            _repositoryFactories.TryGetValue(typeof(T), out factory);
            return factory;
        }

        public Func<IDbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class
        {
            // if we already have this repository in list, return it
            // if not, create new instance of EFRepository
            return GetRepositoryFactory<T>() ?? DefaultEntityRepositoryFactory<T>();
        }

        protected virtual Func<IDbContext, object> DefaultEntityRepositoryFactory<T>() where T : class
        {
            // create new instance of EFRepository<T>
            return dbContext => new EFRepository<T>(dbContext);
        }
    }
}
