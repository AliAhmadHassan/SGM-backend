using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Domain.TypeEmails;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.Receivement;
using SBEISK.SGM.Presentation.API.ViewModels.Response;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ActionAuthorize(ActionPermissions.RecebimentoMaterialDevolvidoDestinatario)]
    public class ReceivementDevolutionController : BaseController
    {
        private readonly IReceivementDevolutionRepository devolutionRepository;
        private readonly IReceivementDevolutionMaterialRepository devolutionMaterialRepository; 
        private readonly IReceivementEmailRepository emailRepository;
        private readonly IReceivementFileRepository fileRepository;

        public ReceivementDevolutionController(IReceivementDevolutionRepository devolutionRepository, IReceivementEmailRepository emailRepository,
                                               IReceivementFileRepository fileRepository, IReceivementDevolutionMaterialRepository devolutionMaterialRepository)
        {
            this.devolutionRepository = devolutionRepository;
            this.emailRepository = emailRepository;
            this.fileRepository = fileRepository;
            this.devolutionMaterialRepository = devolutionMaterialRepository;
        }

        ///<remarks>
        ///Para envio do campo ReceivementDevolutionMaterials, segue exemplo abaixo:
        ///[{
            ///"materialCode": "052.963.448",
            ///"amount": 65,
            ///"MaterialStatusId": 2,
            ///"DevolutionReasonId": 2,
            ///"AdditionalController": "controle"
        ///}]
        ///</remarks>
        [HttpPost]
        public IActionResult PostReceivementDevolution([FromForm] DevolutionRequest request)
        {
            if(ModelState.IsValid)
            {
                if(request != default(DevolutionRequest))
                {
                    ReceivementDevolutionReceiver devolution = Mapper.Map<ReceivementDevolutionReceiver>(request);

                    if(request.Emails != null)
                    {
                        int type = TypeEmails.isDevolutionEmail.GetHashCode();
                        devolution.Emails = this.emailRepository.AddEmail(request.Emails, type).Cast<ReceivementDevolutionEmail>().ToList();
                    }

                    if(request.Attachments != null)
                    {
                        ReceivementDevolutionAttachment attachment = new ReceivementDevolutionAttachment();
                        devolution.Attachments = this.fileRepository.UploadFile(request.Attachments, attachment).Cast<ReceivementDevolutionAttachment>().ToList();
                    }

                    if(!string.IsNullOrEmpty(request.ReceivementDevolutionMaterials))
                    {
                        devolution.DevolutionMaterialsReceiver = this.devolutionMaterialRepository.AddReceivementDevolutionMaterial(request.ReceivementDevolutionMaterials)
                                                                                                  .Cast<ReceivementDevolutionMaterial>().ToList();
                    }

                    this.devolutionRepository.Add(devolution);
                    this.devolutionRepository.SaveChanges();

                    return Ok(devolution.Id.AsSuccessGenericResponse().Success<BaseResponse>("Devolução criada com sucesso."));
                }
            }
            return BadRequest(new BaseResponse().Error<BaseResponse>("Não foi possível criar a devolução."));
        }

        [HttpPost("AsDraft")]
        public IActionResult PostReceivementDevolutionDraft([FromForm] DevolutionDraftRequest request)
        {
            if(ModelState.IsValid)
            {
                if(request != default(DevolutionDraftRequest))
                {
                    ReceivementDevolutionReceiver devolution = Mapper.Map<ReceivementDevolutionReceiver>(request);

                    if(request.Emails != null)
                    {
                        int type = TypeEmails.isDevolutionEmail.GetHashCode();
                        devolution.Emails = this.emailRepository.AddEmail(request.Emails, type).Cast<ReceivementDevolutionEmail>().ToList();
                    }

                    if(request.Attachments != null)
                    {
                        ReceivementDevolutionAttachment attachment = new ReceivementDevolutionAttachment();
                        devolution.Attachments = this.fileRepository.UploadFile(request.Attachments, attachment).Cast<ReceivementDevolutionAttachment>().ToList();
                    }

                    if(!string.IsNullOrEmpty(request.ReceivementDevolutionMaterials))
                    {
                        devolution.DevolutionMaterialsReceiver = this.devolutionMaterialRepository.AddReceivementDevolutionMaterial(request.ReceivementDevolutionMaterials)
                                                                                                  .Cast<ReceivementDevolutionMaterial>().ToList();
                    }

                    this.devolutionRepository.Add(devolution);
                    this.devolutionRepository.SaveChanges();

                    return Ok(devolution.Id.AsSuccessGenericResponse().Success<BaseResponse>("Rascunho salvo com sucesso."));
                }
            }
            return BadRequest(new BaseResponse().Error<BaseResponse>("Não foi possível criar a devolução."));
        }
    }
}