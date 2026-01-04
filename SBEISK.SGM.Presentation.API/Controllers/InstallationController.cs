using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Installation;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Domain.Repositories.Queries;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.Export.Export;
using SBEISK.SGM.Presentation.API.ViewModels.Installation;
using SBEISK.SGM.Presentation.API.ViewModels.Response;
using SBEISK.SGM.Presentation.API.ViewModels.SelectList;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [ActionAuthorize(ActionPermissions.Instalacoes)]
    [Route("api/[controller]")]
    public class InstallationController : BaseController
    {
        private readonly IInstallationRepository installationRepository;
        private readonly IInstallationsReadOnlyRepository installationsRepository;

        public InstallationController(IInstallationRepository installationRepository, IInstallationsReadOnlyRepository installationsRepository)
        {
            this.installationRepository = installationRepository;
            this.installationsRepository = installationsRepository;
        }

        [HttpGet]
        public IActionResult Index(int page, int items, [FromQuery]InstallationQuery filter)
        {
            GenericPaginatedQuery<InstallationQuery> query = new GenericPaginatedQuery<InstallationQuery>(page, items, filter);
            PaginatedQueryResult<Installation> installations = this.installationRepository.All(query);
            PaginatedQueryResult<InstallationViewModel> installationViewModels = installations
                .Transform(x => Mapper.Map<InstallationViewModel>(x));

            return Ok(installationViewModels.AsSuccessGenericResponse());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            Installation installation = installationRepository.Get(id);
            InstallationRequestViewModel installationRequest = Mapper.Map<InstallationRequestViewModel>(installation);
            return Ok(installationRequest.AsSuccessGenericResponse());
        }

        [HttpGet]
        [Route("combo")]
        public IActionResult Combo()
        {
            IList<Installation> installations = installationRepository.All();
            return Ok(SelectItemBuilder.Generate(installations, x => x.Id, x => x.Name).AsSuccessGenericResponse());
        }

        [Produces("text/csv")]
        [Route("instalacoes.csv")]
        [HttpGet]
        [AllowAnonymous]
        [ValidateJwtActionToken(ActionPermissions.Instalacoes)]
        public IActionResult DownloadCsv(string token, [FromQuery]InstallationQuery query)
        {
            IQueryable<Installations> filteredInstallations = installationsRepository.All(query);

            IEnumerable<InstallationExportViewModel> installations = filteredInstallations.Select(x =>
                new InstallationExportViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Type = x.TypeDescription,
                    Project = x.ProjectDescription,
                    Address = x.Address,
                    ThirdMaterial = x.ThirdMaterial,
                }
            );
            return Ok(installations);
        }

        [HttpPost]
        public IActionResult Post(InstallationRequestViewModel installation)
        {
            Installation model = Mapper.Map<Installation>(installation);
            installationRepository.Add(model);
            return Ok(new BaseResponse().Created<BaseResponse>());
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Put(int id, InstallationRequestViewModel installation)
        {
            Installation original = installationRepository.Find(id);
            Installation model = Mapper.Map<Installation>(installation);
            model.Id = id;
            Mapper.Map(model, original);
            return Ok(new BaseResponse().Modified<BaseResponse>());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            installationRepository.Delete(id);
            return Ok(new BaseResponse().Removed<BaseResponse>());
        }
    }
}