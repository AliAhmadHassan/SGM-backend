using System;
using System.Collections.Generic;
using System.Linq;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Projections.User;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.User;
using SBEISK.SGM.Domain.Repositories.Base;
using SBEISK.SGM.Domain.Status;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User UserByEmail(string email);
        int UserId();
        UserRequestProjection NewUser(UserRequestProjection userRequest);
        PaginatedQueryResult<User> All (GenericPaginatedQuery<UserQuery> userQuery);
        UserRequestProjection EditUser(UserRequestProjection userRequest, int id);
        User GetWithInstallations(int id);
        void MergeUsers(ICollection<UserProfileInstallation> original, ICollection<UserProfileInstallation> other, Action<UserProfileInstallation, UserProfileInstallation> updateStrategy);
        IQueryable<User> Query(UserQuery query);
        UserStatus Authorization(string email);
        bool ValidateUserInstallations(string email);
    }
}