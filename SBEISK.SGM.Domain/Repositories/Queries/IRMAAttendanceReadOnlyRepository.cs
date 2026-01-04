
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Repositories.Base;

namespace  SBEISK.SGM.Domain.Repositories.Queries
{
    public interface IRMAAttendanceReadOnlyRepository : IReadOnlyRepository<RMAAttendances>
    {
        PaginatedQueryResult<RMAAttendances> All(GenericPaginatedQuery<RMAQuery> RMAQuery);

         RMAAttendances WithItems(int id);
    }
}