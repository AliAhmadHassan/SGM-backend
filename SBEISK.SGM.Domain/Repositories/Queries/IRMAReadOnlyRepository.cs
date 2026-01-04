using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace SBEISK.SGM.Domain.Repositories.Queries
{
    public interface IRMAReadOnlyRepository : IReadOnlyRepository<RMA>
    {
        PaginatedQueryResult<RMA> All(GenericPaginatedQuery<RMAQuery> RMAQuery);
        IQueryable<RMA> Filter(RMAQuery query);
        RMA GetRMA(int id);
        List<RMAMaterial> GetMaterials(int id);
    }
}