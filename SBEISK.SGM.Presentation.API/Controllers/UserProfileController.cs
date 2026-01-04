using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Domain.Repositories.Queries;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.Profiles;
using SBEISK.SGM.Presentation.API.ViewModels.Response;
using SBEISK.SGM.Presentation.API.ViewModels.SelectList;
using System.Linq;
using SBEISK.SGM.Presentation.API.ViewModels.Profiles.Export;
using SBEISK.SGM.Domain.Queries.UserProfile;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Route("api/[Controller]")]
    [ActionAuthorize(ActionPermissions.Perfis)]
    public class UserProfileController : BaseController
    {
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IProfilePermissionsReadOnlyRepository profilePermissionsReadOnlyRepository;
        
        public UserProfileController(IUserProfileRepository userProfileRepository, IProfilePermissionsReadOnlyRepository profilePermissionsReadOnlyRepository)
        {
            this.userProfileRepository = userProfileRepository;
            this.profilePermissionsReadOnlyRepository = profilePermissionsReadOnlyRepository;
        }

        [HttpGet]
        [Route("Combo")]
        public IActionResult Combo()
        {
            IList<ProfilePermissions> profiles = profilePermissionsReadOnlyRepository.All().Where(p => p.ProfileId > 0).ToList();
            return Ok(SelectItemBuilder.Generate(profiles, x => x.ProfileId, x => x.ProfileName).AsSuccessGenericResponse());
        }

        [HttpGet]
        public IActionResult Index(int page, int items, [FromQuery]UserProfileQuery filter)
        {
            GenericPaginatedQuery<UserProfileQuery> profiles = new  GenericPaginatedQuery<UserProfileQuery>(page, items, filter);
            PaginatedQueryResult<ProfilePermissions> profilesResult = profilePermissionsReadOnlyRepository.All(profiles);
            return Ok(profilesResult.Transform(x => Mapper.Map<ProfilePermissionViewModel>(x)).AsSuccessGenericResponse());
        }
        
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            UserProfile profile = userProfileRepository.GetProfileWithActions(id);
            return Ok(Mapper.Map<UserProfileViewModel>(profile).AsSuccessGenericResponse());
        }

        [Produces("text/csv")]
        [Route("Perfis.csv")] 
        [HttpGet]
        [AllowAnonymous]
        [ValidateJwtActionToken(ActionPermissions.Perfis)]
        public IActionResult DownloadCsv(string token ,[FromQuery]UserProfileQuery filter)
        {
            var profiles = profilePermissionsReadOnlyRepository.Query(filter).Select(x => new UserProfileExportViewModel()
            {
                ProfileName = x.ProfileName,
                ProfileDescription = x.ProfileDescription,
                Permissions = x.Permissions
            });
            return Ok(profiles);
        }

        [HttpPost]
        public IActionResult Post(UserProfileRequest newProfile)
        {
            UserProfile profile = Mapper.Map<UserProfile>(newProfile);
            userProfileRepository.Add(profile);
            return Ok(new BaseResponse().Created<BaseResponse>());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, UserProfileRequest updatedProfile)
        {
            UserProfile original  = userProfileRepository.GetProfileWithActions(id);
            UserProfile newProfile = Mapper.Map<UserProfile>(updatedProfile);
            newProfile.Id = id;

            userProfileRepository.MergePermissions(original.ProfileActions, newProfile.ProfileActions, (orig, other) => Mapper.Map(other, orig));
            Mapper.Map(newProfile, original);
            return Ok(new BaseResponse().Modified<BaseResponse>());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            this.userProfileRepository.Delete(id);
            return Ok(new BaseResponse().Removed<BaseResponse>());
        }
    }
}