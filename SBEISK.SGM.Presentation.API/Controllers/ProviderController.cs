using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Provider;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.Provider;
using SBEISK.SGM.Presentation.API.ViewModels.Provider.Export;
using SBEISK.SGM.Presentation.API.ViewModels.Response;
using SBEISK.SGM.Presentation.API.ViewModels.SelectList;
using System.Collections.Generic;
using System.Linq;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ActionAuthorize(ActionPermissions.Fornecedores)]
    public class ProviderController : BaseController
    {
        private readonly IProviderRepository providerRepository;

        public ProviderController(IProviderRepository providerRepository)
        {
            this.providerRepository = providerRepository;
        }

        [HttpGet]
        public IActionResult Index(int page, int items, [FromQuery]ProviderQuery filter)
        {
            GenericPaginatedQuery<ProviderQuery> query = new GenericPaginatedQuery<ProviderQuery>(page, items, filter);
            PaginatedQueryResult<Provider> providers = providerRepository.All(query);
            PaginatedQueryResult<ProviderViewModel> providerViewModels = providers
                .Transform(x => Mapper.Map<ProviderViewModel>(x));
            return Ok(providerViewModels.AsSuccessGenericResponse());
        }

        [Produces("text/csv")]
        [Route("fornecedores.csv")]
        [HttpGet]
        [AllowAnonymous]
        [ValidateJwtActionToken(ActionPermissions.Fornecedores)]
        public IActionResult DownloadCsv(string token, [FromQuery]ProviderQuery query)
        {
            IQueryable<Provider> filteredProviders = providerRepository.All(query);

            IEnumerable<ProviderExportViewModel> offices = filteredProviders.Select(x =>
                new ProviderExportViewModel() 
                { 
                    Id = x.Id, 
                    CompanyName = x.CompanyName, 
                    Cnpj = x.Cnpj, 
                    Street = x.Street, 
                    Number = x.Number, 
                    Complement = x.Complement, 
                    Neighborhood = x.Neighborhood,
                    City = x.City,
                    Cep = x.Cep
                }
            );
            return Ok(offices);
        }

        [HttpGet("comboProvider")]
        public IActionResult ComboProviders()
        {
            IList<Provider> providers = this.providerRepository.All();
            return Ok(SelectItemBuilder.Generate(providers, p => p.Id, p => $"{p.CompanyName} - {p.Cnpj}").AsSuccessGenericResponse());
        }
    }
}