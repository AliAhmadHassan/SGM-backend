using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.TransferAttendanceMaterial;
using SBEISK.SGM.Domain.Repositories.Queries;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.Response;
using SBEISK.SGM.Presentation.API.ViewModels.TransferAttendanceMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ActionAuthorize(ActionPermissions.RecebimentoMaterialTransferenciaInstalacoes)]
    public class TransferAttendanceMaterialController: BaseController
    {
        private readonly IVwTransferAttendanceMaterialReadOnlyRepository transferAttendanceMaterialReadOnlyRepository;

        public TransferAttendanceMaterialController(IVwTransferAttendanceMaterialReadOnlyRepository transferAttendanceMaterialReadOnlyRepository)
        {
            this.transferAttendanceMaterialReadOnlyRepository = transferAttendanceMaterialReadOnlyRepository;
        }

        [HttpGet("Details/{id}")]
        public IActionResult Get(int id)
        {
            VwTransferAttendanceMaterial transferAttendanceMaterial = this.transferAttendanceMaterialReadOnlyRepository.GetSTM(id);
            List<STMMaterial> materials = this.transferAttendanceMaterialReadOnlyRepository.GetMaterials(id);

            VwTransferAttendanceMaterialResponse transferAttendanceMaterialResponse = Mapper.Map<VwTransferAttendanceMaterialResponse>(transferAttendanceMaterial);
            transferAttendanceMaterialResponse.Materials = Mapper.Map<List<MaterialSTMResponse>>(materials);

            return Ok(transferAttendanceMaterialResponse.AsSuccessGenericResponse());
        }

        [HttpGet]
        public IActionResult Index(int page, int items, [FromQuery]VwTransferAttendanceMaterialQuery filter)
        {
            GenericPaginatedQuery<VwTransferAttendanceMaterialQuery> query = new GenericPaginatedQuery<VwTransferAttendanceMaterialQuery>(page, items, filter);
            PaginatedQueryResult<VwTransferAttendanceMaterial> paginatedQueryResult = this.transferAttendanceMaterialReadOnlyRepository.All(query);
            return Ok(paginatedQueryResult.Transform(x => Mapper.Map<VwTransferAttendanceMaterial>(x)).AsSuccessGenericResponse());
        }
    }
}
