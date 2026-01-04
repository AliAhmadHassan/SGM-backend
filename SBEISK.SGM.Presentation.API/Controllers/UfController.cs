using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.Response;
using SBEISK.SGM.Presentation.API.ViewModels.SelectList;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UfController : BaseController
    {
        private readonly IGenericRepository<Uf> ufRepository;
        private readonly ICityRepository cityRepository;

        public UfController(IGenericRepository<Uf> ufRepository, ICityRepository cityRepository)
        {
            this.ufRepository = ufRepository;
            this.cityRepository = cityRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IList<Uf> ufs = ufRepository.All();
            var response = SelectItemBuilder.Generate(ufs, x => x.Id, x => x.Initials);
            return Ok(response.AsSuccessGenericResponse());
        }

        [HttpGet]
        [Route("{uf:int}/cities")]
        [ResponseCache(Duration = 1800, Location = ResponseCacheLocation.Any)]
        public IActionResult Cities(int uf)
        {
            IList<City> cities = cityRepository.CitiesByUf(uf);
            var response = SelectItemBuilder.Generate(cities, x => x.Id, x => x.Name);
            return Ok(response.AsSuccessGenericResponse());
        }
    }
}