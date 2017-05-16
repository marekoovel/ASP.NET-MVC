using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL.Repositories
{
    // this is universal base EF repository implementation, to be included in all other repos
    // covers all basic crud methods, common for all other repos
    public class EFRepository<T> : IEFRepository<T> where T : class
    {
        // the context and the dbset we are working with
        protected DbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }

        //Constructor, requires dbContext as dependency
        public EFRepository(IDbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");

            DbContext = dbContext as DbContext;
            //get the dbset from context
            DbSet = DbContext.Set<T>();
        }

        public List<T> All { get { return DbSet.ToList();} }

        public List<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            return includeProperties.
                Aggregate<Expression<Func<T, object>>, IQueryable<T>>(DbSet,
                    (current, includeProperty) => current.Include(includeProperty)).ToList();
            /*
        	// non linq version
        	foreach (var includeProperty in includeProperties)
        	{
        		query = query.Include(includeProperty);
        	}
        	return query;
        	*/
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
        }

        public void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return;
            Delete(entity);
        }


        public EntityKey GetPrimaryKeyInfo(T entity)
        {
            var properties = typeof(DbSet).GetProperties();
            foreach (var propertyInfo in properties)
            {
                var objectContext = ((IObjectContextAdapter)DbContext).ObjectContext;
                ObjectStateEntry objectStateEntry;
                if (null != entity && objectContext.ObjectStateManager
                                                   .TryGetObjectStateEntry(entity, out objectStateEntry))
                {
                    return objectStateEntry.EntityKey;
                }
            }
            return null;
        }

        public string[] GetKeyNames(T entity)
        {
            var objectSet = ((IObjectContextAdapter)DbContext).ObjectContext.CreateObjectSet<T>();
            string[] keyNames = objectSet.EntitySet.ElementType.KeyMembers.Select(k => k.Name).ToArray();
            return keyNames;
        }

        public void UpdateOrInsert(T entity)
        {
            var entityKeys = GetKeyNames(entity);
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.Property(entityKeys[0]).CurrentValue == (object)0)
            {
                // insert
                Add(entity);
            }
            else
            {
                // update
                Update(entity);
            }
        }

        public void Dispose()
        {
            if (DbContext == null) return;
            DbContext.Dispose();
            DbContext = null;
        }
    }
}