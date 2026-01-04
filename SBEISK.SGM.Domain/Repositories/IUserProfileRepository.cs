
using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.UserProfile;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        string RoleByPermissionId(int id);
        PaginatedQueryResult<UserProfile> All(GenericPaginatedQuery<UserProfileQuery> query);
        UserProfile GetProfileWithActions(int id);
        void MergePermissions(ICollection<ProfileAction> original, ICollection<ProfileAction> other, Action<ProfileAction, ProfileAction> updateStrategy);
    }
}