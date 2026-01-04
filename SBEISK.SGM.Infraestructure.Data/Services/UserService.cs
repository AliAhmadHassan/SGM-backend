using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Projections.User;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Domain.Services;
using SBEISK.SGM.Infraestructure.Data.Context;

namespace SBEISK.SGM.Infraestructure.Data.Services
{
    public class UserService : IUserService
    {
        private readonly SgmDataContext context;
        private readonly IInstallationRepository installationRepository;
        private const int MasterProfile = -1;

        public UserService(SgmDataContext context, IInstallationRepository installationRepository)
        {
            this.context = context;
            this.installationRepository = installationRepository;
        }
        public IEnumerable<InstalationsBranchOffice> GetBranchOffices(int userId)
        {

            var userbd = this.context.Users.Where(u => u.Id == userId).Include(x => x.UserProfileInstallations)
                                                            .ThenInclude(x => x.UserProfile).SingleOrDefault();
            
            IList<UserInstallationsProfiles> offices = new List<UserInstallationsProfiles>();

            if(userbd.UserProfileInstallations.First().UserProfile.Id == MasterProfile)
                offices = GetMasterInstallations(userbd).ToList();

            else
                offices = context.Query<UserInstallationsProfiles>().Where(x => x.UserId == userId).ToList();
            
            var grouped = offices.GroupBy(x => x.BranchOfficeId);
            foreach (IGrouping<int, UserInstallationsProfiles> group in grouped)
            {
                yield return new InstalationsBranchOffice()
                {
                    Id = group.Key,
                    Description = group.FirstOrDefault().BranchOfficeDescription,
                    Instalations = group.Select(x => new Domain.Projections.User.Installation() { Id = x.InstallationId, Description = x.InstallationDescription, Location = x.Location }).ToList()
                };
            }
        }

        public IEnumerable<UserInstallationsProfiles> GetMasterInstallations(Domain.Entities.User userdb)
        {
            var installation = this.context.Installation.Include(x => x.Address).ThenInclude(x => x.City)
                                                        .Include(x => x.Address).ThenInclude(x => x.Uf)
                                                        .Include(x => x.Project).ThenInclude(x =>x.BranchOffice);

            foreach (Domain.Entities.Installation item in installation)
            {
                yield return new UserInstallationsProfiles()
                {
                    UserId = userdb.Id,
                    InstallationId = item.Id,
                    InstallationDescription = item.Name,
                    ProfileId = -1,
                    BranchOfficeDescription = item.Project.BranchOffice.Description,
                    BranchOfficeId = item.Project.BranchOfficeId,
                    Location = ($"{item.Address.City.Name} - {item.Address.Uf.Initials}")
                };
            }
        }
    }
}
