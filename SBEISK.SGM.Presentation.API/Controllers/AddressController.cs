using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Address;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.Address;
using SBEISK.SGM.Presentation.API.ViewModels.Address.Export;
using SBEISK.SGM.Presentation.API.ViewModels.Response;
using SBEISK.SGM.Presentation.API.ViewModels.SelectList;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ActionAuthorize(ActionPermissions.Enderecos)]
    public class AddressController : BaseController
    {
        private readonly IAddressRepository addressRepository;

        public AddressController(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }
        
        [HttpGet]
        public IActionResult Index(int page, int items, [FromQuery]AddressQuery filter)
        {
            GenericPaginatedQuery<AddressQuery> query = new GenericPaginatedQuery<AddressQuery>(page, items, filter);
            PaginatedQueryResult<Address> addresses = addressRepository.All(query);
            PaginatedQueryResult<AddressViewModel> adressViewModels = addresses.Transform(x => Mapper.Map<AddressViewModel>(x));
            return Ok(adressViewModels.AsSuccessGenericResponse());
        }

        [HttpGet]
        [Route("combo")]
        public IActionResult Combo()
        {
            IList<Address> addresses = addressRepository.All();
            return Ok(SelectItemBuilder.Generate(addresses, x => x.Id, x => x.Description).AsSuccessGenericResponse());
        }


        [Produces("text/csv")]
        [Route("enderecos.csv")]
        [HttpGet]
        [AllowAnonymous]
        [ValidateJwtActionToken(ActionPermissions.Enderecos)]
        public IActionResult DownloadCsv(string token, [FromQuery]AddressQuery query)
        {
            IQueryable<Address> filteredAddress = addressRepository.All(query);

            IEnumerable<AddressExportViewModel> addresses = filteredAddress.Select(x =>
                new AddressExportViewModel() 
                { 
                    PublicPlace = x.PublicPlace, 
                    Description = x.Description, 
                    Number = x.Number, 
                    Neighborhood = x.Neighborhood, 
                    Complement = x.Complement, 
                    Reference = x.Reference, 
                    Cep = x.Cep,
                    City = x.City.Name, 
                    Uf = x.Uf.Name 
                }
            );
            return Ok(addresses);
        }

        [HttpPost]
        public IActionResult Post(AddressRequestViewModel address)
        {
            Address model = Mapper.Map<Address>(address);
            addressRepository.Add(model);
            return Ok(new BaseResponse().Created<BaseResponse>());
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Put(int id, AddressRequestViewModel address)
        {
            Address original = addressRepository.Find(id);
            Address model = Mapper.Map<Address>(address);
            model.Id = id;
            Mapper.Map(model, original);
            return Ok(new BaseResponse().Modified<BaseResponse>());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            addressRepository.Delete(id);
            return Ok(new BaseResponse().Removed<BaseResponse>());
        }
    }
}