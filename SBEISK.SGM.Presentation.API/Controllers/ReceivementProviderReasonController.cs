using System.Collections.Generic;
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
    [ActionAuthorize(ActionPermissions.RecebimentoMaterialFornecedorComNotaSemPedido)]
    public class ReasonWithoutOrderController : BaseController
    {
        private readonly IReasonWithoutOrderRepository reasonWithoutOrderRepository;

        public ReasonWithoutOrderController(IReasonWithoutOrderRepository reasonWithoutOrderRepository)
        {            
            this.reasonWithoutOrderRepository = reasonWithoutOrderRepository;
        }

        [HttpGet("comboProviderReason")]
        public IActionResult ComboProviderReason()
        {
            IList<ReasonWithoutOrder> combo = this.reasonWithoutOrderRepository.All();
            return Ok(SelectItemBuilder.Generate(combo, x => x.Id, x => x.Description).AsSuccessGenericResponse());
        }
    }
}