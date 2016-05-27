using DAL.Interfaces;
using Domain;
using IGBT_DAL.Helpers;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DatabaseContext : DbContext, IDbContext
    {
        [Inject]
        public DatabaseContext()
            : base("DbConnectionStringMusicStore")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DatabaseContext>());

        }

        public IDbSet<Album> Albums { get; set; }
        public IDbSet<Genre> Genres { get; set; }
        public IDbSet<Artist> Artists { get; set; }
        public IDbSet<Cart> Carts { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<OrderDetail> OrderDetails { get; set; }



        public override int SaveChanges()
        {

            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                throw newException;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
