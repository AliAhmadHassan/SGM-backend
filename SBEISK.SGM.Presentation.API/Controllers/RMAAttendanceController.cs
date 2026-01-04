using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Domain.Repositories.Queries;
using SBEISK.SGM.Infraestructure.Data.Repositories;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.Response;
using SBEISK.SGM.Presentation.API.ViewModels.RMA;
using SBEISK.SGM.Presentation.API.ViewModels.RMAAttendance;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ActionAuthorize(ActionPermissions.SaidaMaterialAtendimentoRma)]
    public class RMAAttendanceController : BaseController
    {
        private readonly IRMARepository RMARepository;
        private readonly IRMAReadOnlyRepository RMAReadOnlyRepository;
        private readonly IRMAAttendanceReadOnlyRepository RMAAttendanceReadOnlyRepository;
        private readonly IRMAAttendanceRepository RMAAttendanceRepository;

        public RMAAttendanceController(IRMARepository RMARepository, IRMAReadOnlyRepository RMAReadOnlyRepository,  IRMAAttendanceReadOnlyRepository RMAAttendanceReadOnlyRepository, IRMAAttendanceRepository RMAAttendanceRepository)
        {
            this.RMARepository = RMARepository;
            this.RMAReadOnlyRepository = RMAReadOnlyRepository;
            this.RMAAttendanceRepository = RMAAttendanceRepository;
            this.RMAAttendanceReadOnlyRepository = RMAAttendanceReadOnlyRepository;
        }

        [HttpGet]
        public IActionResult Index(int page, int items, [FromQuery]RMAQuery filter)
        {
            GenericPaginatedQuery<RMAQuery> query = new GenericPaginatedQuery<RMAQuery>(page, items, filter);
            PaginatedQueryResult<RMAAttendances> RMA = this.RMAAttendanceReadOnlyRepository.All(query);
            return Ok(RMA.Transform(x => Mapper.Map<RMAAttendances>(x)).AsSuccessGenericResponse());
        }

        [HttpGet("WithItems")]
        public IActionResult WithItems(int id, [FromQuery]List<decimal> amounts)
        {
            RMAAttendances RMA = RMAAttendanceReadOnlyRepository.WithItems(id);
            RMAAttendancesWithItemsResponse RMAViewModel = Mapper.Map<RMAAttendancesWithItemsResponse>(RMA);
            IList<RMAMaterial> materials = this.RMARepository.RMAMaterials(RMAViewModel.RMAId, amounts);
            RMAViewModel.Materials = Mapper.Map<List<MaterialRequisition>>(materials);
            return Ok(RMAViewModel.AsSuccessGenericResponse());
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public IActionResult Post([FromForm] RMAAttendanceRequest request)
        {
            if (ModelState.IsValid)
            {
                if (request != default(RMAAttendanceRequest))
                {
                    RMAAttendance newAttendance = Mapper.Map<RMAAttendance>(request);

                    newAttendance.Materials = this.RMAAttendanceRepository.NewAttendanceMaterials(request.RMAId, request.ReceivementAmount).ToList();
                    newAttendance.Attachments = Mapper.Map<List<RMAAttendanceAttachments>>(GetByteArray(request.Attachments).ToList());
                    newAttendance.Emails = this.RMAAttendanceRepository.NewAttendanceEmails(request.Emails).ToList();

                    this.RMAAttendanceRepository.Add(newAttendance);
                    return Ok(new BaseResponse().Created<BaseResponse>("Registro finalizado com sucesso"));
                }   
            }
            return BadRequest(new BaseResponse().Error<BaseResponse>("Não foi possível finalizar o registro, verifique todos os dados"));
        }

        [HttpPost("AsDraft")]
        [Consumes("multipart/form-data")]
        public IActionResult PostAsDraft([FromForm] RMAAttendanceAsDraftRequest request)
        {
            if (ModelState.IsValid)
            {
                if (request != default(RMAAttendanceAsDraftRequest))
                {
                    RMAAttendance newAttendance = Mapper.Map<RMAAttendance>(request);

                    newAttendance.Materials = this.RMAAttendanceRepository.NewAttendanceMaterials(request.RMAId, request.ReceivementAmount).ToList();
                    
                    if(newAttendance.Attachments != null)
                    {
                        newAttendance.Attachments = GetByteArray(request.Attachments).Cast<RMAAttendanceAttachments>().ToList();
                    }
                    newAttendance.Emails = this.RMAAttendanceRepository.NewAttendanceEmails(request.Emails).ToList();

                    this.RMAAttendanceRepository.Add(newAttendance);
                    return Ok(new BaseResponse().Created<BaseResponse>("Registro finalizado com sucesso"));
                }   
            }
            return BadRequest(new BaseResponse().Error<BaseResponse>("Não foi possível finalizar o registro, verifique todos os dados"));
        }
    }
}