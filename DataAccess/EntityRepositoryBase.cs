using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using DataAccess.Models;
using DataAccess.Models.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class EntityRepositoryBase<TContext, TEntity> : IEntityRepository<TEntity> where TContext : DbContext, new()
    where TEntity : class, IEntity, new()
    {
        public List<TEntity> GetAll()
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().ToList();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().Where(filter).FirstOrDefault();
            }
        }
    }
}
