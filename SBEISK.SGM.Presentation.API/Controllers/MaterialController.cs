using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Material;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.Material;
using SBEISK.SGM.Presentation.API.ViewModels.Material.Export;
using SBEISK.SGM.Presentation.API.ViewModels.Response;
using System.Linq;
using SBEISK.SGM.Presentation.API.ViewModels.SelectList;
using Microsoft.AspNetCore.Authorization;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [ActionAuthorize(ActionPermissions.Materiais)]
    public class MaterialController : BaseController
    {
        private readonly IMaterialRepository materialRepository;
        private readonly IGenericRepository<MaterialStatus> materialStatusRepository;

        public MaterialController(IMaterialRepository materialRepository, IGenericRepository<MaterialStatus> materialStatusRepository)
        {
            this.materialRepository = materialRepository;
            this.materialStatusRepository = materialStatusRepository;
        }

        [Route("api/[controller]")]
        [HttpGet]
        public IActionResult Index(int page, int items, [FromQuery]MaterialQuery filter)
        {
            GenericPaginatedQuery<MaterialQuery> paginatedQuery = new GenericPaginatedQuery<MaterialQuery>(page, items, filter);
            PaginatedQueryResult<Material> materials = materialRepository.All(paginatedQuery);
            return Ok(materials.Transform(x => Mapper.Map<MaterialViewModel>(x)).AsSuccessGenericResponse());
        }

        [Route("api/Materialstatus/combo")] 
        [HttpGet]
        public IActionResult Combo()
        {
            IList<MaterialStatus> offices = materialStatusRepository.All();
            return Ok(SelectItemBuilder.Generate(offices, x => x.Id, x => x.Description).AsSuccessGenericResponse());
        }
        ///<summary>
        ///data
        ///{
        /// MaterialCode: string,
        /// ReceivementAmount: decimal,
        /// UnityPrice: decimal
        ///}
        ///</summary>
        [Route("api/MaterialCode/combo")]
        [HttpGet]
        public IActionResult ComboWithoutOrder([FromQuery]MaterialQuery filter)
        {
            IQueryable<Material> materials = this.materialRepository.Query(filter).Take(10);
            return Ok(materials.Select(x => new { x.Code, x.Description, x.Unity }).AsSuccessGenericResponse());
        }

        [Produces("text/csv")]
        [Route("api/[controller]/Material.csv")] 
        [HttpGet]
        [AllowAnonymous]
        [ValidateJwtActionToken(ActionPermissions.Materiais)]
        public IActionResult DownloadCsv(string token, [FromQuery]MaterialQuery filter)
        {
            var materials = materialRepository.Query(filter).Select(x => new MaterialExportViewModel()
            {
                Code = x.Code,
                Description = x.Description,
                Unit = x.Unity
            });
            return Ok(materials);
        }
    }
}