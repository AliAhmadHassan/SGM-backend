using System.Collections.Generic;
using System.Linq;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Repositories.Queries;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories.Queries
{
    public class UserInstallationsProfilesReadOnlyRepository : ReadOnlyRepository<UserInstallationsProfiles>, IUserInstallationsProfilesReadOnlyRepository
    {
        public UserInstallationsProfilesReadOnlyRepository(SgmDataContext dataContext) : base(dataContext)
        {

        }
        public IList<UserInstallationsProfiles> ByUserId(int userId)
        {
            return base.Query(u => u.UserId == userId).ToList();
        }
    }
}