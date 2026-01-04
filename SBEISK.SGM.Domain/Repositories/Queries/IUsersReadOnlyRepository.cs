using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.User;
using SBEISK.SGM.Domain.Repositories.Base;
using System.Collections.Generic;

namespace SBEISK.SGM.Domain.Repositories.Queries
{
    public interface IUsersReadOnlyRepository : IReadOnlyRepository<Users>
    {
        PaginatedQueryResult<Users> All(GenericPaginatedQuery<UserQuery> userQuery);
    }
}
