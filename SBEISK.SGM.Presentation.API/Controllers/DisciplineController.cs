using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.Response;
using SBEISK.SGM.Presentation.API.ViewModels.SelectList;
using System.Collections.Generic;


namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Authorize]
    public class DisciplineController : BaseController
    {
        private readonly IGenericRepository<Discipline> disciplineRepository;

        public DisciplineController(IGenericRepository<Discipline> disciplineRepository)
        {
            this.disciplineRepository = disciplineRepository;
        }

        [HttpGet]
        [Route("api/[controller]/combo")]
        public IActionResult Combo()
        {
            IList<Discipline> offices = this.disciplineRepository.All();
            return Ok(SelectItemBuilder.Generate(offices, x => x.Id, x => x.Description).AsSuccessGenericResponse());
        }
    }
}