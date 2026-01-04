

using System.Linq;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.UserProfile;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Domain.Repositories.Queries
{
    public interface IProfilePermissionsReadOnlyRepository : IReadOnlyRepository<ProfilePermissions>
    {
        PaginatedQueryResult<ProfilePermissions> All(GenericPaginatedQuery<UserProfileQuery> query);

        IQueryable<ProfilePermissions> Query(UserProfileQuery query);
    }
}