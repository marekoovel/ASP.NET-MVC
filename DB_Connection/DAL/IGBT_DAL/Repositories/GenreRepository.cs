using DAL.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class GenreRepository : EFRepository<Genre>, IGenreRepository
    {
        public GenreRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public Genre GetGenreWithAlbums(string genre)
        {
            var res = DbSet.Include("Albums").Single(g => g.Name == genre);
            return res;
        }
    }
}
