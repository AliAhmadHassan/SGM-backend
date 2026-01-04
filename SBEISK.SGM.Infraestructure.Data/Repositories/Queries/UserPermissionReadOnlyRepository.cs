using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Repositories.Queries;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace SBEISK.SGM.Infraestructure.Data.Repositories.Queries
{
    public class UserPermissionReadOnlyRepository : ReadOnlyRepository<UserPermission>, IUserPermissionReadOnlyRepository
    {
        public UserPermissionReadOnlyRepository(SgmDataContext dataContext) : base(dataContext)
        {
        }

        public IList<UserPermission> ByUserIdByInstallation(int userId, int? installationId)
        {
            return base.Query(x => x.UserId == userId && x.installationId == installationId).ToList();
        }
    }
}
