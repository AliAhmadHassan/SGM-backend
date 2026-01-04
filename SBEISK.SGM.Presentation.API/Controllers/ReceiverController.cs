using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Provider;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.Project;
using SBEISK.SGM.Presentation.API.ViewModels.Provider.Export;
using SBEISK.SGM.Presentation.API.ViewModels.Receiver;
using SBEISK.SGM.Presentation.API.ViewModels.Response;
using SBEISK.SGM.Presentation.API.ViewModels.SelectList;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ActionAuthorize(ActionPermissions.Destinatarios)]
    
    public class ReceiverController : BaseController
    {
        private readonly IReceiverRepository receiverRepository;

        public ReceiverController(IReceiverRepository receiverRepository)
        {
            this.receiverRepository = receiverRepository;
        }

        [HttpGet]
        public IActionResult Index(int page, int items, [FromQuery]ReceiverQuery filter)
        {
            GenericPaginatedQuery<ReceiverQuery> query =  new GenericPaginatedQuery<ReceiverQuery>(page, items, filter);
            PaginatedQueryResult<Receiver> receivers = receiverRepository.All(query);
            PaginatedQueryResult<ReceiverViewModel> receiversViewModels = receivers.Transform(x => Mapper.Map<ReceiverViewModel>(x));
            return Ok(receiversViewModels.AsSuccessGenericResponse());
        }

        
        [HttpPost]
        public IActionResult Post(ReceiverRequestViewModel receiver)
        {     
            Receiver model = Mapper.Map<Receiver>(receiver);
            receiverRepository.Add(model);
            return Ok(new BaseResponse().Created<BaseResponse>());
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Put(int id, ReceiverRequestViewModel receiver)
        {
            Receiver model = Mapper.Map<Receiver>(receiver);
            model.Id = id;
            receiverRepository.Update(model);
            return Ok(new BaseResponse().Modified<BaseResponse>());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            receiverRepository.Delete(id);
            return Ok(new BaseResponse().Removed<BaseResponse>());
        }

        [Produces("text/csv")]
        [Route("destinatarios.csv")]
        [HttpGet]
        [AllowAnonymous]
        [ValidateJwtActionToken(ActionPermissions.Destinatarios)]
        public IActionResult DownloadCsv(string token, [FromQuery]ReceiverQuery query)
        {
            IQueryable<ReceiverExportViewModel> receivers = receiverRepository.Query(query).Select(x =>
                new ReceiverExportViewModel() 
                { 
                    Code = x.Id,
                    Description = x.Description,
                    Address = x.Address, 
                    ReceiverType = x.ReceiverType.Description
                }
            );
            return Ok(receivers);
        }

        [HttpGet("{id}")]
        public IActionResult ReceiverById(int id)
        {
            Receiver receiver = this.receiverRepository.Find(id);

            if(receiver != default(Receiver))
                return Ok(receiver.AsSuccessGenericResponse());
            return NotFound(new BaseResponse().Error<BaseResponse>("Não foi localizado nenhum destinatário."));
        }

        [HttpGet("Code/combo")]
        public IActionResult Combo()
        {
            IList<Receiver> projects = this.receiverRepository.All();
            return Ok(SelectItemBuilder.Generate(projects, x => x.Id, x => x.Id).AsSuccessGenericResponse());
        }
    }
}