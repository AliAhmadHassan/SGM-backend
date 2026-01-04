using System.Collections.Generic;
using System.Linq;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Provider;
using SBEISK.SGM.Domain.Repositories.Base;

namespace  SBEISK.SGM.Domain.Repositories
{
    public interface IProviderRepository : IRepository<Provider>
    {
        IList<Provider> WithItems();
        PaginatedQueryResult<Provider> All(GenericPaginatedQuery<ProviderQuery> query);
        IQueryable<Provider> All(ProviderQuery query);
    }
}