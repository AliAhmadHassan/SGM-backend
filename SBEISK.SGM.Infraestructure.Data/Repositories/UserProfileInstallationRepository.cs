using System.Collections.Generic;
using System.Linq;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Projections.UserProfileInstallations;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class UserProfileInstallationRepository : Repository<UserProfileInstallation>, IUserProfileInstallationRepository
    {
        public UserProfileInstallationRepository(SgmDataContext dataContext) : base(dataContext)
        {

        }

        public UserProfileInstallation NewUserProfileInstallation(int userId, Association association, bool active = true)
        {
            UserProfileInstallation UserProfileInstallation = Add(new UserProfileInstallation
            {
                UserId = userId,
                InstallationId = association.InstallationId,
                UserProfileId = association.UserProfileId
            });

            SaveChanges();
            return UserProfileInstallation;
        }

        public List<UserProfileInstallation> UserProfileInstallationsById(int id)
        {
            return this.DbSet.Where(ui => ui.UserId == id).ToList();
        }

        /* public void EditUserInstallation(int userId, UserRequestProjection request)
        {
            List<UserProfileInstallation> userProfileInstallations = UserProfileInstallationsById(userId);

            if (request.InstallationId.Length == userProfileInstallations.Count)
            {
                foreach (UserProfileInstallation item in userProfileInstallations)
                {
                    if (request.InstallationId.Contains(item.InstallationId))
                    {
                        //item.UserInstallationApprover = request.Active.Value;
                        SaveChanges();
                    }
                }
            }
            else
            {
                DbSet.RemoveRange(userProfileInstallations);
                foreach (int insId in request.InstallationId)
                {
                    NewUserProfileInstallation(userId, insId);
                }
            }
        } */
    }
}