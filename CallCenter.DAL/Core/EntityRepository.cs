using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.DAL.Core
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class, new()
    {
        private CallCenterContext context;

        public EntityRepository(CallCenterContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("Context is null.");
            }
            this.context = context;
        }

        public CallCenterContext GetContext()
        {
            return this.context;
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public void Delete(int id)
        {
            T targetEntity = context.Set<T>().Find(id);
            context.Entry(targetEntity).State = EntityState.Deleted;
        }

        public void Edit(T entity)
        {
            DbEntityEntry entry = context.Entry<T>(entity);
            entry.State = EntityState.Modified;
            //context.Entry<T>(entity).CurrentValues.SetValues(entity);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return context.Set<T>().AsNoTracking<T>().AsQueryable<T>();
        }

        public async Task<T> GetSingle(Guid key)
        {
            var result = await Task.Run(() =>
            {
                return context.Set<T>().Find(key);
            });
            return result;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
