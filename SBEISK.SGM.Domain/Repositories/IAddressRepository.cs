using System.Linq;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Address;
using SBEISK.SGM.Domain.Repositories.Base;

namespace  SBEISK.SGM.Domain.Repositories
{
    public interface IAddressRepository : IRepository<Address>
    {
        PaginatedQueryResult<Address> All(GenericPaginatedQuery<AddressQuery> query);
        IQueryable<Address> All(AddressQuery query);
    }
}