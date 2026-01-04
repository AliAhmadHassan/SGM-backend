using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.Response;
using SBEISK.SGM.Presentation.API.ViewModels.SelectList;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ActionAuthorize(ActionPermissions.Destinatarios)]
    public class ReceiverTypeController : BaseController
    {
        private readonly IGenericRepository<ReceiverType> receiverTypeRepository;

        public ReceiverTypeController(IGenericRepository<ReceiverType> receiverTypeRepository)
        {
            this.receiverTypeRepository = receiverTypeRepository;
        }
        
        [HttpGet("combo")]
        public IActionResult Combo()
        {
            IList<ReceiverType> types = receiverTypeRepository.All();
            return Ok(SelectItemBuilder.Generate(types, x => x.Id, x => x.Description).AsSuccessGenericResponse());
        }
    }
}