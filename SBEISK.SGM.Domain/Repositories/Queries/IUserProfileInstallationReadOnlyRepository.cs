using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Repositories.Base;
using System.Collections.Generic;

namespace SBEISK.SGM.Domain.Repositories.Queries
{
    public interface IUserInstallationsProfilesReadOnlyRepository : IReadOnlyRepository<UserInstallationsProfiles>
    {
        IList<UserInstallationsProfiles> ByUserId(int userId);
    }
}