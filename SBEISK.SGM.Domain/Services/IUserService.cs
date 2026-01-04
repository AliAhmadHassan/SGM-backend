using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Projections.User;

namespace SBEISK.SGM.Domain.Services
{
    public interface IUserService
    {
        IEnumerable<InstalationsBranchOffice> GetBranchOffices(int userId);
        IEnumerable<UserInstallationsProfiles> GetMasterInstallations(Domain.Entities.User userdb);
    }
}
