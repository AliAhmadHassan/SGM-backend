using System.Linq;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.BranchOffice;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface IBranchOfficeRepository : IRepository<BranchOffice>
    {
        PaginatedQueryResult<BranchOffice> All(GenericPaginatedQuery<BranchOfficeQuery> query);
        IQueryable<BranchOffice> All(BranchOfficeQuery query);
        bool IsUniqueName (string name, int? id = null);
    }
}
