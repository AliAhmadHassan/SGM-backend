
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.ViewModels.Response;
using SBEISK.SGM.Presentation.API.ViewModels.SelectList;

namespace SBEISK.SGM.Presentation.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ActionAuthorize(ActionPermissions.Instalacoes)]
    public class InstallationTypeController  : BaseController
    {
        private readonly IInstallationTypeRepository installationTypeRepository;

        public InstallationTypeController(IInstallationTypeRepository installationTypeRepository)
        {
            this.installationTypeRepository = installationTypeRepository;
        }

        [HttpGet]
        [Route("combo")]
        public IActionResult Combo()
        {
            IList<InstallationType> installationTypes = installationTypeRepository.All();
            return Ok(SelectItemBuilder.Generate(installationTypes, x => x.Id, x => x.Description).AsSuccessGenericResponse());
        }
    }
}