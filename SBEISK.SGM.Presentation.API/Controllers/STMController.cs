using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Domain.TypeEmails;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.Response;
using SBEISK.SGM.Presentation.API.ViewModels.STM;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ActionAuthorize(ActionPermissions.RecebimentoMaterialTransferenciaInstalacoes)]
    public class STMController : BaseController
    {
        private readonly ISTMRepository stmRepository;
        private readonly IReceivementFileRepository fileRepository;
        private readonly IReceivementEmailRepository emailRepository;
        private readonly ISTMMaterialRepository sTMMaterialRepository;
        public STMController(ISTMRepository stmRepository, IReceivementFileRepository fileRepository, IReceivementEmailRepository emailRepository,
                            ISTMMaterialRepository sTMMaterialRepository)
        {
            this.stmRepository = stmRepository;
            this.fileRepository = fileRepository;
            this.emailRepository = emailRepository;
            this.stmRepository = stmRepository;
            this.sTMMaterialRepository = sTMMaterialRepository;
        }

        ///<summary>
        ///No campo MaterialCodesAmount, é enviado um json no seguinte formado: [{"materialCode": "258.235.147", "amount": 100},
        ///                                                                      {"materialCode": "209.036.884", "amount": 50}]
        ///</summary>
        [HttpPost]
        [Consumes("multipart/form-data")]
        public IActionResult PostSTM([FromForm] STMRequestViewModel request)
        {
            if(ModelState.IsValid)
            {
                if(request != default(STMRequestViewModel))
                {
                    STM stm = Mapper.Map<STM>(request);

                    if(request.Attachments != null)
                    {
                        STMAttachment attachment = new STMAttachment();
                        stm.Attachments = this.fileRepository.UploadFile(request.Attachments, attachment).Cast<STMAttachment>().ToList();
                    }

                    if(request.Emails != null)
                    {
                        int typeEmail = TypeEmails.isSTMEmail.GetHashCode();
                        stm.Emails = this.emailRepository.AddEmail(request.Emails, typeEmail).Cast<STMEmail>().ToList();
                    }

                    if(request.MaterialCodesAmount != null)
                    {
                        stm.STMMaterials = this.sTMMaterialRepository.AddSTMMaterial(request.MaterialCodesAmount).ToList();
                    }
                    else
                    {
                        return BadRequest(new BaseResponse().Error<BaseResponse>("Não foi informado nenhum material na solicitação."));
                    }

                    this.stmRepository.Add(stm);
                    this.stmRepository.SaveChanges();
                    return Ok(stm.Id.AsSuccessGenericResponse().Created<BaseResponse>("Solicitação de transferência realizada com sucesso."));
                }
            }
            return BadRequest(new BaseResponse().Error<BaseResponse>("Não foi possível finalizar a solicitação de transferencia."));
        }

        [HttpPost("AsDraft")]
        [Consumes("multipart/form-data")]
        public IActionResult PostSTMDraft([FromForm] STMRequestDraft request)
        {
            if(ModelState.IsValid)
            {
                if(request != default(STMRequestDraft))
                {
                    STM stm = Mapper.Map<STM>(request);

                    if(request.Attachments != null)
                    {
                        STMAttachment attachment = new STMAttachment();
                        stm.Attachments = this.fileRepository.UploadFile(request.Attachments, attachment).Cast<STMAttachment>().ToList();
                    }

                    if(request.Emails != null)
                    {
                        int typeEmail = TypeEmails.isSTMEmail.GetHashCode();
                        stm.Emails = this.emailRepository.AddEmail(request.Emails, typeEmail).Cast<STMEmail>().ToList();
                    }

                    if(request.MaterialCodesAmount != null)
                    {
                        stm.STMMaterials = this.sTMMaterialRepository.AddSTMMaterial(request.MaterialCodesAmount).ToList();
                    }
                    else
                    {
                        return BadRequest(new BaseResponse().Error<BaseResponse>("Não foi informado nenhum material na solicitação."));
                    }

                    this.stmRepository.Add(stm);
                    this.stmRepository.SaveChanges();
                    return Ok(stm.Id.AsSuccessGenericResponse().Created<BaseResponse>("Rascunho salvo com sucesso."));
                }
            }
            return BadRequest(new BaseResponse().Error<BaseResponse>("Não foi possível finalizar a solicitação de transferencia."));
        }

        [HttpGet]
        public IActionResult Index(int page, int items, [FromQuery] STMQuery queryFilter)
        {
            GenericPaginatedQuery<STMQuery> query = new GenericPaginatedQuery<STMQuery>(page, items, queryFilter);
            PaginatedQueryResult<STM> queryResult = this.stmRepository.All(query);
            PaginatedQueryResult<STMViewModel> viewModel = queryResult.Transform(map => Mapper.Map<STMViewModel>(map));
            return Ok(viewModel.AsSuccessGenericResponse());
        }

        [HttpGet("{id}")]
        public IActionResult STMById(int id)
        {
            STM stm = this.stmRepository.STMById(id);
            STMViewModel viewModel = Mapper.Map<STMViewModel>(stm);
            return Ok(viewModel.AsSuccessGenericResponse());
        }

        [HttpPatch("approveSTM/{id}")]
        public IActionResult ApproveSTM(int id, [FromBody] JsonPatchDocument<STM> status)
        {
            STM stm = this.stmRepository.Find(id);

            if(stm != default(STM))
            {
                status.ApplyTo(stm, ModelState);
                return Ok();
            }
            return NotFound(new BaseResponse().Error<BaseResponse>("Não foi possível localizar a solicitação desejada."));
        }
    }
}
