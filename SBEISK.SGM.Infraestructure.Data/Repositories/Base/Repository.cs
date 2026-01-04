using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.Domain.Entities.Base;
using SBEISK.SGM.Domain.Exceptions;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Repositories.Base;
using SBEISK.SGM.Infraestructure.Data.Context;

namespace SBEISK.SGM.Infraestructure.Data.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        public SgmDataContext DataContext { get; set; }
        public DbSet<TEntity> DbSet { get; set; }

        public Repository(SgmDataContext dataContext)
        {
            this.DataContext = dataContext;
            this.DbSet = this.DataContext.Set<TEntity>();
        }

        public virtual TEntity Add(TEntity entity)
        {
            this.DbSet.Add(entity);
            return entity;
        }

        public virtual IList<TEntity> All()
        {
            return this.DbSet.ToList();
        }

        public void Delete(TEntity entity)
        {
            this.DbSet.Remove(entity);
        }

        public virtual void Delete(int id)
        {
            TEntity entity = this.Find(id);
            if (entity == null)
            {
                throw new EntityNotFoundException($"Entity {typeof(TEntity).Name} with given key was not found.");
            }

            this.DbSet.Remove(entity);
        }

        public TEntity Find(params object[] keys)
        {
            return this.DbSet.Find(keys);
        }

        public IQueryable<TEntity> Query()
        {
            return this.DbSet.AsNoTracking().AsQueryable();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbSet.AsNoTracking().Where(predicate);
        }

        public IQueryable<TEntity> Query(PaginatedQuery query)
        {
            var items = this.DbSet.AsNoTracking().OrderBy(x => x.Id).Skip((Math.Max(query.Page, 1) - 1) * query.Items).Take(query.Items);
            var ListPage = new PaginatedQueryResult<TEntity>(items.ToList(), this.DbSet.Count());
            return ListPage.Items.AsQueryable();
        }


        public TEntity Update(TEntity entity)
        {
            TEntity dbEntity = this.DbSet.Where(x => x.Id == entity.Id).AsNoTracking().FirstOrDefault();
            if (dbEntity == null)
            {
                throw new EntityNotFoundException($"Entity {typeof(TEntity).Name} with given key was not found.");
            }

            this.DbSet.Attach(entity).State = EntityState.Modified;

            return entity;
        }

        public PaginatedQueryResult<TEntity> All(PaginatedQuery query)
        {
            var items = this.DbSet.OrderBy(x => x.Id).Skip((Math.Max(query.Page, 1) - 1) * query.Items).Take(query.Items);
            return new PaginatedQueryResult<TEntity>(items.ToList(), this.DbSet.Count());
        }

        protected PaginatedQueryResult<TEntity> All(PaginatedQuery query, Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> queryable = this.DbSet.Where(predicate);
            var items = queryable.OrderBy(x => x.Id).Skip((Math.Max(query.Page, 1) - 1) * query.Items).Take(query.Items);
            return new PaginatedQueryResult<TEntity>(items.ToList(), queryable.Count());
        }

        protected PaginatedQueryResult<TEntity> ApplyPagination(IQueryable<TEntity> queryable, PaginatedQuery query)
        {
            var items = queryable.OrderBy(x => x.Id).Skip((Math.Max(query.Page, 1) - 1) * query.Items).Take(query.Items);
            return new PaginatedQueryResult<TEntity>(items.ToList(), queryable.Count());
        }

        public bool SaveChanges()
        {
            return this.DataContext.SaveChanges() > 0;
        }
    }
}
