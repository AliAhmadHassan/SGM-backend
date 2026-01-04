using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SBEISK.SGM.Domain.Repositories.Base
{
    public interface IReadOnlyRepository<TEntity> where TEntity : class
    {
        IList<TEntity> All();
        IQueryable<TEntity> Query();
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);

    }
}
