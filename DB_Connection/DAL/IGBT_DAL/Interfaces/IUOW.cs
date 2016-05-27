using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUOW
    {
        //save pending changes to the data store
        void Commit();
        void RefreshAllEntities();
        //UOW Methods, that dont fit into specific repo
        //get repository for type
        T GetRepository<T>() where T : class;
        IEFRepository<Album> Albums { get; }
        IEFRepository<Genre> Genres { get; }
        IEFRepository<Artist> Artists { get; }
        IEFRepository<Cart> Carts { get; }
        IEFRepository<OrderDetail> OrderDetails { get; }
        IEFRepository<Order> Orders { get; }
        IGenreRepository Genre { get; }
        IAlbumRepository Album { get; }
        ICartRepository Cart { get; }
        IOrderRepository Order { get; }
    }
}
