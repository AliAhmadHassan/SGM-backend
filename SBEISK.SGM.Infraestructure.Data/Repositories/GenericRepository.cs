using SBEISK.SGM.Domain.Entities.Base;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class GenericRepository<TEntity> : Repository<TEntity>, IGenericRepository<TEntity> where TEntity : Entity
    {
        public GenericRepository(SgmDataContext dataContext) : base(dataContext) { }
    }
}
