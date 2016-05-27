using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IGBT880_DAL.Interfaces
{
    // this is the base repository interface for all EF repositories
    public interface IEFRepository<T> : IDisposable where T : class
    {
        // gett all records in table
        IQueryable<T> All { get; }

        // get all records with filter
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);

        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void UpdateOrInsert(T entity);
        void Delete(T entity);
        void Delete(int id);
        // <summary>
        // This should be removed for UoW pattern, since saving to datastore is in UoW implementation
        // </summary>
        //void Save();
    }
}