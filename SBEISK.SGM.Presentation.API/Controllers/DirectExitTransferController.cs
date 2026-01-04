using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Domain.TypeEmails;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.DirectExitTransfer;
using SBEISK.SGM.Presentation.API.ViewModels.Response;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ActionAuthorize(ActionPermissions.SaidaMaterialDiretaTransferencia)]
    public class DirectExitTransferController : BaseController
    {
        private readonly IDirectExitRepository directExitRepository;
        private readonly IReceivementFileRepository fileRepository;
        private readonly IReceivementEmailRepository emailRepository;
        private readonly IExitMaterialRepository exitMaterialRepository;

        public DirectExitTransferController(IDirectExitRepository directExitRepository, IReceivementFileRepository fileRepository,
                                            IReceivementEmailRepository emailRepository, IExitMaterialRepository exitMaterialRepository)
        {
            this.directExitRepository = directExitRepository;
            this.fileRepository = fileRepository;
            this.emailRepository = emailRepository;
            this.exitMaterialRepository = exitMaterialRepository;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public IActionResult PostDirectExitTransfer([FromForm] DirectExitTransferRequest request)
        {
            if(ModelState.IsValid)
            {
                if(request != default(DirectExitTransferRequest))
                {
                    DirectExit directExit = Mapper.Map<DirectExit>(request);

                    if(!this.directExitRepository.ValidateDate(directExit))
                        return BadRequest(new BaseResponse().Error<BaseResponse>("A data de embarque não pode ser posterior a de entrega."));

                    if(!string.IsNullOrEmpty(request.Emails))
                    {
                        int type = TypeEmails.isDirectExitEmail.GetHashCode();
                        directExit.Emails = this.emailRepository.AddEmail(request.Emails, type).Cast<ExitEmail>().ToList();
                    }

                    if(request.Attachments != null)
                    {
                        ExitAttachment attachment = new ExitAttachment();
                        directExit.Attachments = this.fileRepository.UploadFile(request.Attachments, attachment).Cast<ExitAttachment>().ToList();
                    }

                    if(request.Photos != null)
                    {
                        ExitPhotoBoarding photo = new ExitPhotoBoarding();
                        directExit.Photos = this.fileRepository.UploadFile(request.Photos, photo).Cast<ExitPhotoBoarding>().ToList();
                        if(directExit.Photos.Any(x => x == default(ExitPhotoBoarding)))
                            return BadRequest(new BaseResponse().Error<BaseResponse>("Somente é permitido arquivos .JPG e/ou .PNG para upload de fotos"));
                    }

                    if(!string.IsNullOrEmpty(request.MaterialCodesAmounts))
                    {
                        directExit.ExitMaterials = this.exitMaterialRepository.AddExitMaterial(request.MaterialCodesAmounts).Cast<ExitMaterial>().ToList();
                    }

                    this.directExitRepository.Add(directExit);
                    this.directExitRepository.SaveChanges();

                    return Ok(directExit.Id.AsSuccessGenericResponse().Created<BaseResponse>("Saída direta para transferência finalizada com sucesso."));
                }
            }
            return BadRequest(new BaseResponse().Error<BaseResponse>("Não foi possível finalizar a criação da saída. Por favor, revise os dados."));
        }

        [HttpPost("AsDraft")]
        [Consumes("multipart/form-data")]
        public IActionResult PostDirectExitTransferDraft([FromForm] DirectExitTransferDraftRequest request)
        {
            if(ModelState.IsValid)
            {
                if(request != default(DirectExitTransferDraftRequest))
                {
                    DirectExit directExit = Mapper.Map<DirectExit>(request);

                    if(!this.directExitRepository.ValidateDate(directExit))
                        return BadRequest(new BaseResponse().Error<BaseResponse>("A data de embarque não pode ser posterior a de entrega."));

                    if(!string.IsNullOrEmpty(request.Emails))
                    {
                        int type = TypeEmails.isDirectExitEmail.GetHashCode();
                        directExit.Emails = this.emailRepository.AddEmail(request.Emails, type).Cast<ExitEmail>().ToList();
                    }

                    if(request.Attachments != null)
                    {
                        ExitAttachment attachment = new ExitAttachment();
                        directExit.Attachments = this.fileRepository.UploadFile(request.Attachments, attachment).Cast<ExitAttachment>().ToList();
                    }

                    if(request.Photos != null)
                    {
                        ExitPhotoBoarding photo = new ExitPhotoBoarding();
                        directExit.Photos = this.fileRepository.UploadFile(request.Photos, photo).Cast<ExitPhotoBoarding>().ToList();
                        if(directExit.Photos.Any(x => x.Equals(default(ExitPhotoBoarding))))
                            return BadRequest(new BaseResponse().Error<BaseResponse>("Somente é permitido arquivos .JPG e/ou .PNG para upload de fotos"));
                    }

                    if(!string.IsNullOrEmpty(request.MaterialCodesAmounts))
                    {
                        directExit.ExitMaterials = this.exitMaterialRepository.AddExitMaterial(request.MaterialCodesAmounts).Cast<ExitMaterial>().ToList();
                    }

                    this.directExitRepository.Add(directExit);
                    this.directExitRepository.SaveChanges();

                    return Ok(directExit.Id.AsSuccessGenericResponse().Created<BaseResponse>("Rascunho salvo com sucesso."));
                }
            }
            return BadRequest(new BaseResponse().Error<BaseResponse>("Não foi possível finalizar a criação da saída. Por favor, revise os dados."));
        }
    }
}