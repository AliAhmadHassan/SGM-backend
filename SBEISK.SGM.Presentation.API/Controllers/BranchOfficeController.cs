using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.BranchOffice;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.BranchOffice;
using SBEISK.SGM.Presentation.API.ViewModels.BranchOffice.Export;
using SBEISK.SGM.Presentation.API.ViewModels.Response;
using SBEISK.SGM.Presentation.API.ViewModels.SelectList;
using System.Collections.Generic;
using System.Linq;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Authorize]
    public class BranchOfficeController : BaseController
    {
        private readonly IBranchOfficeRepository branchOfficeRepository;

        public BranchOfficeController(IBranchOfficeRepository branchOfficeRepository)
        {
            this.branchOfficeRepository = branchOfficeRepository;
        }

        [HttpGet]
        [Route("api/[controller]")]
        [ActionAuthorize(ActionPermissions.Filiais)]
        public IActionResult Index(int page, int items, [FromQuery]BranchOfficeQuery filter)
        {
            GenericPaginatedQuery<BranchOfficeQuery> query = new GenericPaginatedQuery<BranchOfficeQuery>(page, items, filter);
            PaginatedQueryResult<BranchOffice> branchOffices = branchOfficeRepository.All(query);
            PaginatedQueryResult<BranchOfficeViewModel> branchOfficeViewModels = branchOffices
                .Transform(x => Mapper.Map<BranchOfficeViewModel>(x));

            return Ok(branchOfficeViewModels.AsSuccessGenericResponse());
        }

        [HttpGet]
        [Route("api/[controller]/combo")]
        public IActionResult Combo()
        {
            IList<BranchOffice> offices = branchOfficeRepository.All();
            return Ok(SelectItemBuilder.Generate(offices, x => x.Id, x => x.Description).AsSuccessGenericResponse());
        }

        [Produces("text/csv")]
        [Route("api/[controller]/filiais.csv")]
        [HttpGet]
        [AllowAnonymous]
        [ValidateJwtActionToken(ActionPermissions.Filiais)]
        public IActionResult DownloadCsv(string token, [FromQuery]BranchOfficeQuery query)
        {
            IQueryable<BranchOffice> filteredBranchOffice = branchOfficeRepository.All(query);

            IEnumerable<BranchOfficeExportViewModel> branchOffices = filteredBranchOffice.Select(x =>
                new BranchOfficeExportViewModel()
                {
                    Id = x.Id,
                    FantasyName = x.FantasyName,
                    Name = x.Description,
                    Street = x.Street,
                    Number = x.Number,
                    Neighborhood = x.Neighborhood,
                    Cep = x.Cep,
                    City = x.City,
                    Uf = x.Uf,
                }
            );
            return Ok(branchOffices);
        }

        [HttpPost]
        [Route("api/[controller]")]
        [ActionAuthorize(ActionPermissions.Filiais)]
        public IActionResult Post(BranchOfficeViewModel office)
        {
            if(!(branchOfficeRepository.IsUniqueName(office.Name)))
                return BadRequest(new BaseResponse().Error<BaseResponse>("Já existe uma filial com esse nome"));
            
            BranchOffice p = Mapper.Map<BranchOffice>(office);
            branchOfficeRepository.Add(p);
            return Ok(new BaseResponse().Created<BaseResponse>());
        }

        [HttpPut]
        [Route("api/[controller]/{id:int}")]
        [ActionAuthorize(ActionPermissions.Filiais)]
        public IActionResult Put(int id, BranchOfficeViewModel office)
        {
            if(!(branchOfficeRepository.IsUniqueName(office.Name, id)))
                return BadRequest(new BaseResponse().Error<BaseResponse>("Já existe uma filial com esse nome"));

            BranchOffice p = Mapper.Map<BranchOffice>(office);
            p.Id = id;
            branchOfficeRepository.Update(p);
            return Ok(new BaseResponse().Modified<BaseResponse>());
        }

        [HttpDelete]
        [Route("api/[controller]/{id:int}")]
        [ActionAuthorize(ActionPermissions.Filiais)]
        public IActionResult Delete(int id)
        {
            branchOfficeRepository.Delete(id);
            return Ok(new BaseResponse().Removed<BaseResponse>());
        }
    }
}