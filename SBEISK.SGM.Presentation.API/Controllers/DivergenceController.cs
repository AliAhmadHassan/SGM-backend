using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Storage.Base;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.Divergence;
using SBEISK.SGM.Presentation.API.ViewModels.Response;
using SBEISK.SGM.Presentation.API.ViewModels.SelectList;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class DivergenceController : BaseController
    {
        private readonly IDivergenceRepository divergenceRepository;
        private readonly IGenericRepository<DivergenceType> divergenceTypeRepository;
        private readonly IDivergenceFileRepository divergenceFilesRepository;

        public DivergenceController(IDivergenceRepository divergenceRepository, IGenericRepository<DivergenceType> divergenceTypeRepository, IDivergenceFileRepository divergenceFilesRepository)
        {
            this.divergenceRepository = divergenceRepository;
            this.divergenceTypeRepository = divergenceTypeRepository;
            this.divergenceFilesRepository = divergenceFilesRepository;
        }

        [HttpPost]
        public IActionResult Post([FromForm]DivergenceRequestViewModel divergence)
        {
            if(divergenceTypeRepository.Find(divergence.StatusId) == null)
                return BadRequest();

            Divergence model = Mapper.Map<Divergence>(divergence);
            this.divergenceRepository.Add(model);
            this.divergenceRepository.SaveChanges();
            this.divergenceFilesRepository.SaveFiles(divergence.File, model.Id); 

            return Ok(new BaseResponse().Created<BaseResponse>());
        }

        [HttpGet]
        [Route("Combo")]

        public IActionResult Combo()
        {
            IList<DivergenceType> divergenceTypes = this.divergenceTypeRepository.All();
            return Ok(SelectItemBuilder.Generate(divergenceTypes, x => x.Id , x => x.Description).AsSuccessGenericResponse());
        }
    }
}
