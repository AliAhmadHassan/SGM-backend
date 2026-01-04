using System.Linq;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Provider;
using SBEISK.SGM.Domain.Repositories.Base;

namespace  SBEISK.SGM.Domain.Repositories
{
    public interface IReceiverRepository : IRepository<Receiver>
    {
        PaginatedQueryResult<Receiver> All(GenericPaginatedQuery<ReceiverQuery> query);

         IQueryable<Receiver> Query(ReceiverQuery query);
    }
}