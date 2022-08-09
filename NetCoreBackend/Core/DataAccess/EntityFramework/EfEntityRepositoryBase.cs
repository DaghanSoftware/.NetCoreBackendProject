using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntitiy, TContext> : IEntityRepository<TEntitiy>
        where TEntitiy : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntitiy entity)
        {
            using (var context = new TContext())
            {
               var addedEntity= context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntitiy entity)
        {
            using (var context = new TContext())
            {
                var deleteEntity = context.Entry(entity);
                deleteEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntitiy Get(Expression<Func<TEntitiy, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntitiy>().SingleOrDefault(filter);
            }
        }

        public IList<TEntitiy> GetList(Expression<Func<TEntitiy, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntitiy>().ToList()
                    : context.Set<TEntitiy>().Where(filter).ToList();
            }
        }

        public void Update(TEntitiy entity)
        {
            using (var context = new TContext())
            {
                var updateEntity = context.Entry(entity);
                updateEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
