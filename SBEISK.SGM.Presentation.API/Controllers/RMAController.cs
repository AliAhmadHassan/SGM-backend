using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Projections.RMA;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Domain.Repositories.Queries;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.Response;
using SBEISK.SGM.Presentation.API.ViewModels.RMA;
using SBEISK.SGM.Presentation.API.ViewModels.SelectList;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ActionAuthorize(ActionPermissions.CriarDocumentoRma)]
    public class RMAController : BaseController
    {
        private readonly IRMARepository RMARepository;
        private readonly IRMAReadOnlyRepository RMAReadOnlyRepository;
        private readonly IRMADetailsReadOnlyRepository DetailsRepository;
        private readonly IGenericRepository<RMAStatus> RMAStatusRepository;

        public RMAController(IRMARepository RMARepository, IRMAReadOnlyRepository RMAReadOnlyRepository,  IRMADetailsReadOnlyRepository DetailsRepository, IGenericRepository<RMAStatus> RMAStatusRepository)
        {
            this.RMARepository = RMARepository;
            this.RMAReadOnlyRepository = RMAReadOnlyRepository;
            this.DetailsRepository = DetailsRepository;
            this.RMAStatusRepository = RMAStatusRepository;
        }

        ///<summary>
        ///O envio do campo MaterialWithoutOrder, é feito no formato Json. Exemplo: [{"materialCode": "00.000.00000", 
        ///                                                                                           "amountReceived": 0.00, 
        ///                                                                                           "disciplineId": 0}]
        ///</summary>
        [HttpPost]
        public IActionResult CreateDocument([FromForm] RMARequest request)
        {
            RequisitionOfMaterialForApplication newRMA = Mapper.Map<RequisitionOfMaterialForApplication>(request);
            
            if (string.IsNullOrEmpty(request.Materials))
            {
                return Ok(new BaseResponse().Error<BaseResponse>("Lista de materiais vazia. Preencha todos os campos"));
            }

            List<RMAMaterialProjection> materialRequest = JsonConvert.DeserializeObject<List<RMAMaterialProjection>>(request.Materials).ToList();
            newRMA.Materials = this.RMARepository.NewMaterials(materialRequest).ToList();

            if(request.Files != null)
            {
                newRMA.RMAattachments = Mapper.Map<List<RMAattachments>>(GetByteArray(request.Files)).ToList();
            }
            this.RMARepository.Add(newRMA);

            return Ok(new BaseResponse().Created<BaseResponse>());
        }

        [HttpGet("Details/{id}")]
        public IActionResult Get(int id)
        {
            RMA RMAOriginal = this.RMAReadOnlyRepository.GetRMA(id);
            List<RMAMaterial> materials = this.RMAReadOnlyRepository.GetMaterials(id);

            RMAResponse RMAResponse = Mapper.Map<RMAResponse>(RMAOriginal);
            RMAResponse.Materials = Mapper.Map<List<MaterialRMAResponse>>(materials);

            return  Ok(RMAResponse.AsSuccessGenericResponse());
        }

        [HttpGet("FinalDetails/{id}")]
        public IActionResult Details(int id)
        {
            RMADetails RMAOriginal = this.DetailsRepository.GetRMA(id);
            List<RMAMaterial> materials = this.RMAReadOnlyRepository.GetMaterials(id);

            RMADetailsResponse RMAResponse = Mapper.Map<RMADetailsResponse>(RMAOriginal);
            RMAResponse.Materials = Mapper.Map<List<MaterialRMAResponse>>(materials);
            
            return  Ok(RMAResponse.AsSuccessGenericResponse());
        }

        [HttpGet]
        public IActionResult Index(int page, int items, [FromQuery]RMAQuery filter)
        {
            GenericPaginatedQuery<RMAQuery> query = new GenericPaginatedQuery<RMAQuery>(page, items, filter);
            PaginatedQueryResult<RMA> RMA = this.RMAReadOnlyRepository.All(query);
            return Ok(RMA.Transform(x => Mapper.Map<RMA>(x)).AsSuccessGenericResponse());
        }

        ///<summary>
        ///O envio do campo MaterialWithoutOrder, é feito no formato Json. Exemplo: [{"materialCode": "00.000.00000", 
        ///                                                                                           "amountReceived": 0.00, 
        ///                                                                                           "disciplineId": 0}]
        ///</summary>
        [HttpPatch("ApproveOrReprove/{id}")]
        public IActionResult AproveRMA(int id, [FromBody]JsonPatchDocument<RequisitionOfMaterialForApplication> RMArequest)
        {
            RequisitionOfMaterialForApplication RMAdb = this.RMARepository.Find(id);
            RMArequest.ApplyTo(RMAdb, ModelState);
            return Ok();
        }

        [HttpGet("Status/combo")]
        public IActionResult Combo()
        {
            IList<RMAStatus> projects = this.RMAStatusRepository.All();
            return Ok(SelectItemBuilder.Generate(projects, x => x.Id, x => x.Description).AsSuccessGenericResponse());
        }
    }
}