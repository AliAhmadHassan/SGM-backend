using System.Linq;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Material;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface IMaterialRepository : IRepository<Material>
    {
        PaginatedQueryResult<Material> All(GenericPaginatedQuery<MaterialQuery> query);
        IQueryable<Material> Query(MaterialQuery query);
        Material MaterialByCode(string materialCode);
    }
}