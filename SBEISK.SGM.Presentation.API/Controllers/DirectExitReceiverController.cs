using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Domain.TypeEmails;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels;
using SBEISK.SGM.Presentation.API.ViewModels.Response;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ActionAuthorize(ActionPermissions.SaidaMaterialDiretaDestinatario)]
    public class DirectExitReceiverController : BaseController
    {
        private readonly IDirectExitReceiverRepository directExitReceiverRepository; 
        private readonly IReceivementEmailRepository emailRepository;
        private readonly IReceivementFileRepository fileRepository;
        private readonly IDirectExitReceiverMaterialRepository exitMaterialRepository;
        
        public DirectExitReceiverController(IDirectExitReceiverRepository directExitReceiverRepository, IReceivementEmailRepository emailRepository,
                                      IReceivementFileRepository fileRepository, IDirectExitReceiverMaterialRepository exitMaterialRepository)
        {
            this.directExitReceiverRepository = directExitReceiverRepository;
            this.emailRepository = emailRepository;
            this.fileRepository = fileRepository;
            this.exitMaterialRepository = exitMaterialRepository;
        }

        ///<remarks>
        /// Para envio do campo MaterialCodesAmounts, segue exemplo:
        /// [{
        ///     "Amount": 50,
        ///     "MaterialCode": "558.489.222",
        ///     "DisciplineId": 2
        /// }]
        ///</remarks>
        [HttpPost]
        public IActionResult CreateDirectExitReceiver([FromForm] DirectExitReceiverRequest request)
        {
            if(ModelState.IsValid)
            {
                if(request != default(DirectExitReceiverRequest))
                {
                    DirectExitReceiver exitReceiver = Mapper.Map<DirectExitReceiver>(request);

                    if(request.Emails != null)
                    {
                        int type = TypeEmails.isDirectExitReceiverEmail.GetHashCode();
                        exitReceiver.Emails = this.emailRepository.AddEmail(request.Emails, type).Cast<DirectExitReceiverEmail>().ToList();
                    }

                    if(request.Attachments != null)
                    {
                        DirectExitReceiverAttachment attachment = new DirectExitReceiverAttachment();
                        exitReceiver.Attachments = this.fileRepository.UploadFile(request.Attachments, attachment).Cast<DirectExitReceiverAttachment>().ToList();
                    }

                    if(request.MaterialCodesAmounts != null)
                    {
                        exitReceiver.DirectExitReceiverMaterials = this.exitMaterialRepository.AddExitReceiverMaterial(request.MaterialCodesAmounts).Cast<DirectExitReceiverMaterial>().ToList();
                    }

                    this.directExitReceiverRepository.Add(exitReceiver);
                    this.directExitReceiverRepository.SaveChanges();

                    return Ok(exitReceiver.Id.AsSuccessGenericResponse().Success<BaseResponse>("Saída para destinatário criada com sucesso."));
                }
            }
            
            return BadRequest(new BaseResponse().Error<BaseResponse>("Não foi possível criar saída para destinatário."));
        }
    }
}