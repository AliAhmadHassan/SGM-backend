using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Project;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.Project;
using SBEISK.SGM.Presentation.API.ViewModels.Provider.Export;
using SBEISK.SGM.Presentation.API.ViewModels.Response;
using SBEISK.SGM.Presentation.API.ViewModels.SelectList;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ActionAuthorize(ActionPermissions.Projetos)]
    public class ProjectController : BaseController
    {
        private readonly IProjectRepository projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }
        
        [HttpGet]
        public IActionResult Index(int page, int items, [FromQuery]ProjectQuery filter)
        {
            GenericPaginatedQuery<ProjectQuery> query =  new GenericPaginatedQuery<ProjectQuery>(page, items, filter);
            PaginatedQueryResult<Project> projects = projectRepository.All(query);
            PaginatedQueryResult<ProjectViewModel> andressViewModels = projects.Transform(x => Mapper.Map<ProjectViewModel>(x));
            return Ok(andressViewModels.AsSuccessGenericResponse());
        }

        [HttpPost]
        public IActionResult Post(ProjectRequestViewModel project)
        {     
            Project model = Mapper.Map<Project>(project);
            projectRepository.Add(model);
            return Ok(new BaseResponse().Created<BaseResponse>());
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Put(int id, ProjectRequestViewModel project)
        {
            Project model = projectRepository.Find(id);            
            Project newModel = Mapper.Map<Project>(project);
            newModel.Id = id;
            Mapper.Map(newModel, model);
            return Ok(new BaseResponse().Modified<BaseResponse>());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            projectRepository.Delete(id);
            return Ok(new BaseResponse().Removed<BaseResponse>());
        }

        [HttpGet("combo")]
        public IActionResult Combo()
        {
            IList<Project> projects = projectRepository.All();
            return Ok(SelectItemBuilder.Generate(projects, x => x.Id, x => x.Description).AsSuccessGenericResponse());
        }

        [Produces("text/csv")]
        [Route("projetos.csv")]
        [HttpGet]
        [AllowAnonymous]
        [ValidateJwtActionToken(ActionPermissions.Projetos)]
        public IActionResult DownloadCsv(string token, [FromQuery]ProjectQuery query)
        {
            IQueryable<Project> filteredProviders = projectRepository.Query(query);

            IEnumerable<ProjectExportViewModel> offices = filteredProviders.Select(x =>
                new ProjectExportViewModel() 
                { 
                    Id = x.Id,
                    Description = x.Description, 
                    Initials = x.Initials, 
                    BranchOffice = x.BranchOffice.Description,
                    Active = x.Active ? "Sim" : "Não"
                }
            );
            return Ok(offices);
        }
    }
}