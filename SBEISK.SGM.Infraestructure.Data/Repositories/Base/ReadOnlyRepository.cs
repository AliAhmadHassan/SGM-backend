using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Repositories.Base;
using SBEISK.SGM.Infraestructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SBEISK.SGM.Infraestructure.Data.Repositories.Base
{
    public abstract class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        public SgmDataContext DataContext { get; set; }
        public DbQuery<TEntity> DbQuery { get; set; }

        protected ReadOnlyRepository(SgmDataContext dataContext)
        {
            this.DataContext = dataContext;
            this.DbQuery = this.DataContext.Query<TEntity>();
        }

        public IList<TEntity> All()
        {
            return this.DbQuery.ToList();
        }

        public IQueryable<TEntity> Query()
        {
            return this.DbQuery.AsQueryable();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbQuery.Where(predicate);
        }

        protected PaginatedQueryResult<TEntity> ApplyPagination(IQueryable<TEntity> queryable, PaginatedQuery query)
        {
            var items = queryable.Skip((Math.Max(query.Page, 1) - 1) * query.Items).Take(query.Items);
            return new PaginatedQueryResult<TEntity>(items.ToList(), queryable.Count());
        }
    }
}
