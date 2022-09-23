using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity,TContext>
        where TEntity : class,IEntity,new()
        where TContext : DbContext,new()
    {
        public void Add(TEntity entity)
        {
            using (TContext serviceContext = new TContext())
            {
                var addedEntity = serviceContext.Entry(entity);
                addedEntity.State = EntityState.Added;
                serviceContext.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext serviceContext = new TContext())
            {
                var deletedEntity = serviceContext.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                serviceContext.SaveChanges();
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext serviceContext = new TContext())
            {
                return filter == null ?
                    serviceContext.Set<TEntity>().ToList() :
                    serviceContext.Set<TEntity>().Where(filter).ToList();
            }
        }
        public  TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext serviceContext=new TContext())
            {
                return serviceContext.Set<TEntity>().FirstOrDefault(filter);

            }
        }

        public void Update(TEntity entity)
        {
            using (TContext serviceContext = new TContext())
            {
                var modifiedEntity = serviceContext.Entry(entity);
                modifiedEntity.State = EntityState.Modified;
                serviceContext.SaveChanges();
            }
        }
    }
}
