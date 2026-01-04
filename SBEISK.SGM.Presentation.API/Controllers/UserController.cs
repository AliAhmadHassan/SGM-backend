using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Projections.User;
using SBEISK.SGM.Domain.Projections.UserProfileInstallations;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.User;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Domain.Services;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels;
using SBEISK.SGM.Presentation.API.ViewModels.Address.Export;
using SBEISK.SGM.Presentation.API.ViewModels.Menu;
using SBEISK.SGM.Presentation.API.ViewModels.Response;
using SBEISK.SGM.Presentation.API.ViewModels.SelectList;
using SBEISK.SGM.Presentation.API.ViewModels.User;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ActionAuthorize(ActionPermissions.Usuarios)]
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        private readonly IUserRepository userRepository;
        private readonly IUserProfileInstallationRepository userInstallationRepository;
        private readonly IAuthenticationService authorizationService;
        

        public UserController(IUserService userService, IAuthenticationService authorizationService, IUserRepository userRepository, IUserProfileInstallationRepository userInstallationRepository)
        {
            this.userService = userService;
            this.authorizationService = authorizationService;
            this.userRepository= userRepository;
            this.userInstallationRepository = userInstallationRepository;
        }

        [HttpGet]
        [Route("BranchOffices")]
        public IActionResult BranchOffices()
        {
            IList<InstalationsBranchOffice> branchOffices = userService.GetBranchOffices(base.GetUserId()).ToList();
            IList<BranchOfficeViewModel> branchOfficesViewModel = branchOffices.Select(x => Mapper.Map<BranchOfficeViewModel>(x)).ToList();
            return Ok(branchOfficesViewModel.AsSuccessGenericResponse());
        }

        [HttpGet]
        public IActionResult Index(int page, int items, [FromQuery] UserQuery userQuery)
        {
            GenericPaginatedQuery<UserQuery> query = new GenericPaginatedQuery<UserQuery>(page, items, userQuery);
            PaginatedQueryResult<User> users = this.userRepository.All(query);
            PaginatedQueryResult<UserViewModel> usersViewModel = users.Transform(map => Mapper.Map<UserViewModel>(map));
            return Ok(usersViewModel.AsSuccessGenericResponse());
        }

        [HttpGet]
        [Route("UsersAD")]
        public async System.Threading.Tasks.Task<IActionResult> getAllUserAsync()
        {
            var user = await authorizationService.GetUsers();
            
            return Ok(user);
        }

        [HttpPost]
        public IActionResult NewUser(UserRequestViewModel userRequest)
        {
            UserRequestProjection userProjection = Mapper.Map<UserRequestProjection>(userRequest);
            
            UserRequestProjection newUser = this.userRepository.NewUser(userProjection);

            if(newUser != null)
            {
                int userId = this.userRepository.UserId();

                foreach (var item in userRequest.Associations)
                {
                    this.userInstallationRepository.NewUserProfileInstallation(userId, Mapper.Map<Association>(item));
                }

                return Ok(new BaseResponse().Created<BaseResponse>());
            }
            return BadRequest(new BaseResponse().Error<BaseResponse>("Este usuário já foi cadastrado!"));
        }

        [HttpPut("{id}")]
        public IActionResult EditUser(UserRequestViewModel userRequest, int id)
        {
            User model = Mapper.Map<User>(userRequest);
            model.Id = id;    
            User original = userRepository.GetWithInstallations(id);

            if(original.UserProfileInstallations.Count != 0)
            {
                if(original.UserProfileInstallations.First().UserProfileId == -1)
                {
                    return Unauthorized( new BaseResponse().Error<BaseResponse>("Esse usuário não pode ser editado"));
                }
            }
            
            userRepository.MergeUsers(original.UserProfileInstallations, model.UserProfileInstallations, (orig, other) => Mapper.Map(other, orig));
            Mapper.Map(model, original);
            return Ok(new BaseResponse().Modified<BaseResponse>());
        }

        [HttpGet]
        [Route("combo")]
        public IActionResult Combo()
        {
            IList<User> users = userRepository.All();
            return Ok(SelectItemBuilder.Generate(users, x => x.Id, x => x.Name).AsSuccessGenericResponse());
        }

        [Produces("text/csv")]
        [Route("Usuarios.csv")]
        [HttpGet]
        [AllowAnonymous]
        [ValidateJwtActionToken(ActionPermissions.Usuarios)]
        public IActionResult DownloadCsv(string token, [FromQuery]UserQuery filter)
        {
            IEnumerable<UserExportViewModel> offices = this.userRepository.Query(filter).Select(x =>
                new UserExportViewModel() 
                { 
                    Name = x.Name, 
                    NameInstallations = string.Join(", ", x.UserProfileInstallations.Select(ins => ins.Installation.Name)), 
                    Active = x.Active ? "Ativo" : "Inativo"
                }
            );
            return Ok(offices);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteUser(int id)
        {
            this.userRepository.Delete(id);
            return Ok(new BaseResponse().Removed<BaseResponse>());
        }

        [HttpGet]
        [Route("teste")]
        public IActionResult teste()
        {
            IList<User> users = userRepository.All();
            return Ok(users);
        }
    }
}