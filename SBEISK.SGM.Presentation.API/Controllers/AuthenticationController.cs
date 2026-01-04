using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Queries.Login;
using SBEISK.SGM.Domain.Queries.Login.Results;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Domain.Status;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.Login;
using SBEISK.SGM.Presentation.API.ViewModels.Response;
using System.Threading.Tasks;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    
    public class AuthenticationController : BaseController
    {
        private readonly Domain.Services.IAuthenticationService authorizationService;
        private readonly IUserRepository userRepository;

        public AuthenticationController(Domain.Services.IAuthenticationService authorizationService, IUserRepository userRepository)
        {
            this.authorizationService = authorizationService;
            this.userRepository = userRepository;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            LoginQuery loginQuery = Mapper.Map<LoginQuery>(model);
            LoginQueryResult result = await authorizationService.Authenticate(loginQuery);
            if (result.Succeeded)
            {
                if( UserStatus.Activated != userRepository.Authorization(loginQuery.Username))
                    return Unauthorized(new BaseResponse().Error<BaseResponse>("Usuário inativo ou deletado"));
                
                if(!userRepository.ValidateUserInstallations(loginQuery.Username))
                    return BadRequest(new BaseResponse()
                        .Error<BaseResponse>("Não identificamos um perfil ou instalação vinculada ao seu usuário. Por favor, contate um administrador do sistema para configurar o seu perfil."));
                        
                return Ok(result.Token.AsSuccessGenericResponse());
            }
            else if(!string.IsNullOrEmpty(result.Message) && !result.Succeeded)
                return NotFound(new BaseResponse().Error<BaseResponse>(result.Message));

            return BadRequest(new BaseResponse().Error<BaseResponse>("Dados Inválidos"));
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Authorization/{id}")]
        [Authorize]
        public async Task<IActionResult> ApplyPermissions(int id)
        {
            int userId = GetUserId();
            string token = await authorizationService.Authorize(userId, id);
            if (token == string.Empty)
                return Unauthorized(new BaseResponse().Error<BaseResponse>("O seu usuário não possui permissão para acessar essa instalação"));
            return Ok(token.AsSuccessGenericResponse());
        }
    }
}
