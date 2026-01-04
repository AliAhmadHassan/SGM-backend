using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Projections.User;
using SBEISK.SGM.Domain.Projections.UserProfileInstallations;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface IUserProfileInstallationRepository : IRepository<UserProfileInstallation>
    {
        UserProfileInstallation NewUserProfileInstallation(int userId, Association association, bool active = true);
        //void EditUserInstallation(int userId, UserRequestProjection request);
        List<UserProfileInstallation> UserProfileInstallationsById(int id);
    }
}