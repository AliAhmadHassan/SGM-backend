using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Projections;
using SBEISK.SGM.Domain.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace SBEISK.SGM.Domain.Repositories.Queries
{
    public interface IParentPermissionReadOnlyRepository : IReadOnlyRepository<ParentPermissions>
    {
        IEnumerable<ParentPermissionsProjection> GetPermissions();
    }
}