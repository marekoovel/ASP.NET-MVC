using DAL.Interfaces;
using DAL.Repositories;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL.Repositories
{
    public class AlbumRepository : EFRepository<Album>, IAlbumRepository
    {
        public AlbumRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public List<Album> GetTopAlbums(int count)
        {
            return DbSet.OrderByDescending(a => a.OrderDetails.Count()).Take(count).ToList();
        }

        public List<Album> GetAlbums()
        {
           return DbSet.Include(a => a.Genre).Include(a => a.Artist).ToList();
        }
    }
}
