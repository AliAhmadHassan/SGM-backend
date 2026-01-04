using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Repositories.Queries;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using System.Linq;
using SBEISK.SGM.Presentation.API.ViewModels.Action;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Presentation.API.ViewModels.SelectList;
using SBEISK.SGM.Presentation.API.ViewModels.Response;

namespace SBEISK.SGM.Presentation.API.Controllers
{   
    [ActionAuthorize(ActionPermissions.Perfis)]
    public class ActionController : BaseController
    {
        private readonly IParentPermissionReadOnlyRepository parentPermissionReadOnlyRepository;
        private readonly IGenericRepository<Domain.Entities.Action> actionRepository;

        public ActionController(IParentPermissionReadOnlyRepository parentPermissionReadOnlyRepository, IGenericRepository<Domain.Entities.Action> actionRepository)
        {
            this.parentPermissionReadOnlyRepository = parentPermissionReadOnlyRepository;
            this.actionRepository = actionRepository;
        }

        [HttpGet]
        [Route("api/[Controller]")]
        public IActionResult GetPermissions()
        {
            var permission = parentPermissionReadOnlyRepository.GetPermissions();
            return Ok(permission.Select(Mapper.Map<ParentActionViewModel>));
        }

        [HttpGet]
        [Route("api/Parent[Controller]/Combo")]
        public IActionResult Combo()
        {
            var parentActions = actionRepository.All().Where(x => x.ParentActionId == null).ToList();
            return Ok(SelectItemBuilder.Generate(parentActions, x => x.Id, x => x.Description).AsSuccessGenericResponse());
        }
    }
}