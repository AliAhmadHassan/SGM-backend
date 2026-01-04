using SBEISK.SGM.Domain.Entities.Base;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface IGenericRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {

    }
}