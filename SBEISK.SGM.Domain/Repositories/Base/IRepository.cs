using SBEISK.SGM.Domain.Queries;
using System.Linq;

namespace SBEISK.SGM.Domain.Repositories.Base
{
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(int id);
        TEntity Find(params object[] keys);
        bool SaveChanges();
        PaginatedQueryResult<TEntity> All(PaginatedQuery query);
        IQueryable<TEntity> Query(PaginatedQuery query);
    }
}
