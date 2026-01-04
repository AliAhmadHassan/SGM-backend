using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Repositories.Base;
using System.Collections.Generic;

namespace SBEISK.SGM.Domain.Repositories.Queries
{
    public interface IUserPermissionReadOnlyRepository : IReadOnlyRepository<UserPermission>
    {
        IList<UserPermission> ByUserIdByInstallation(int userId, int? installationId);
    }
}
