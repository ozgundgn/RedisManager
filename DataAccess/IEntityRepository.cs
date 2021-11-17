using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess
{
    public interface IEntityRepository<TEntity>
    {
        List<TEntity> GetAll();
        TEntity Get(Expression<Func<TEntity, bool>> filter);
    }
}
