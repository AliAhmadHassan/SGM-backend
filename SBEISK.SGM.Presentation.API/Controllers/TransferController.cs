using System.Linq;
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
using SBEISK.SGM.Presentation.API.ViewModels.Transfer;

namespace SBEISK.SGM.Presentation.API
{
    [Route("api/[controller]")]
    [ActionAuthorize(ActionPermissions.RecebimentoMaterialTransferenciaInstalacoes)]
    public class TransferController : BaseController
    {
        private readonly ITransferRepository transferRepository;
        private readonly IReceivementFileRepository fileRepository;
        private readonly IReceivementEmailRepository emailRepository;
        private readonly ITransferMaterialRepository transferMaterialRepository;
        public TransferController(ITransferRepository transferRepository, IReceivementFileRepository fileRepository, IReceivementEmailRepository emailRepository,
                                  ITransferMaterialRepository transferMaterialRepository)
        {
            this.transferRepository = transferRepository;
            this.fileRepository = fileRepository;
            this.emailRepository = emailRepository;
            this.transferMaterialRepository = transferMaterialRepository;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public IActionResult PostTransfer([FromForm] TransferRequestViewModel request)
        {
            if (ModelState.IsValid)
            {
                if (request != default(TransferRequestViewModel))
                {
                    Transfer transfer = Mapper.Map<Transfer>(request);

                    if (request.Emails != null)
                    {
                        int type = TypeEmails.isTransferEmail.GetHashCode();
                        transfer.Emails = this.emailRepository.AddEmail(request.Emails, type).Cast<TransferEmail>().ToList();
                    }

                    if (request.Attachments != null)
                    {
                        TransferAttachment attachment = new TransferAttachment();
                        transfer.Attachments = this.fileRepository.UploadFile(request.Attachments, attachment).Cast<TransferAttachment>().ToList();
                    }

                    if(request.Photos != null)
                    {
                        TransferPhoto photos = new TransferPhoto();
                        transfer.Photos = this.fileRepository.UploadFile(request.Photos, photos).Cast<TransferPhoto>().ToList();
                    }

                    transfer.TransferMaterials = this.transferMaterialRepository.AddTransferMaterial(request.STMId).ToList();

                    this.transferRepository.Add(transfer);
                    this.transferRepository.SaveChanges();
                    
                    return Ok(transfer.Id.AsSuccessGenericResponse().Created<BaseResponse>("Transferência realizada com sucesso."));
                }
            }
            return BadRequest(new BaseResponse().Error<BaseResponse>("Não foi possível finalizar a transferência."));
        }

        [HttpPost("asDraft")]
        [Consumes("multipart/form-data")]
        public IActionResult PostTransferDraft([FromForm] TransferRequestDraftViewModel request)
        {
            if (ModelState.IsValid)
            {
                if (request != default(TransferRequestDraftViewModel))
                {
                    Transfer transfer = Mapper.Map<Transfer>(request);

                    if (request.Emails != null)
                    {
                        int type = TypeEmails.isTransferEmail.GetHashCode();
                        transfer.Emails = this.emailRepository.AddEmail(request.Emails, type).Cast<TransferEmail>().ToList();
                    }

                    if (request.Attachments != null)
                    {
                        TransferAttachment attachment = new TransferAttachment();
                        transfer.Attachments = this.fileRepository.UploadFile(request.Attachments, attachment).Cast<TransferAttachment>().ToList();
                    }

                    if(request.Photos != null)
                    {
                        TransferPhoto photos = new TransferPhoto();
                        transfer.Photos = this.fileRepository.UploadFile(request.Photos, photos).Cast<TransferPhoto>().ToList();
                    }

                    transfer.TransferMaterials = this.transferMaterialRepository.AddTransferMaterial(request.STMId).ToList();

                    this.transferRepository.Add(transfer);
                    this.transferRepository.SaveChanges();
                    
                    return Ok(transfer.Id.AsSuccessGenericResponse().Created<BaseResponse>("Transferência realizada com sucesso."));
                }
            }
            return BadRequest(new BaseResponse().Error<BaseResponse>("Não foi possível finalizar a transferência."));
        }

        [HttpGet]
        public IActionResult Index(int page, int items, [FromQuery] STMQuery queryFilter)
        {
            GenericPaginatedQuery<STMQuery> query = new GenericPaginatedQuery<STMQuery>(page, items, queryFilter);
            PaginatedQueryResult<Transfer> queryResult = this.transferRepository.All(query);
            PaginatedQueryResult<STMViewModel> viewModel = queryResult.Transform(map => Mapper.Map<STMViewModel>(map));
            return Ok(viewModel.AsSuccessGenericResponse());
        }

        [HttpGet("{id}")]
        public IActionResult TransferById(int id)
        {
            Transfer transfer = this.transferRepository.TransferById(id);
            STMViewModel viewModel = Mapper.Map<STMViewModel>(transfer);
            return Ok(viewModel.AsSuccessGenericResponse());
        }
    }
}