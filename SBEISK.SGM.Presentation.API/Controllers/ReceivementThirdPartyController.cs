using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Projections.Receivement;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Domain.Services;
using SBEISK.SGM.Domain.TypeEmails;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.ReceivementInvoiceOrder;
using SBEISK.SGM.Presentation.API.ViewModels.Response;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ActionAuthorize(ActionPermissions.RecebimentoMaterialTerceiros)]
    public class ReceivementThirdPartyController : BaseController
    {
        private readonly IReceivementInvoiceOrderRepository receivementInvoiceOrderRepository;
        private readonly IReceivementEmailRepository receivementEmailRepository;
        private readonly IReceivementFileRepository receivementFileRepository;
        private readonly IReceivementService receivementService;
        private readonly IReceivementMaterialRepository receivementMaterialRepository;
        private readonly IReceivementAttachmentRepository receivementAttachmentRepository;
        private readonly IReceivementProviderReasonRepository receivementProviderReasonRepository;
        public ReceivementThirdPartyController(IReceivementInvoiceOrderRepository receivementInvoiceOrderRepository,
                                               IReceivementEmailRepository receivementEmailRepository,
                                               IReceivementFileRepository receivementFileRepository,
                                               IReceivementService receivementService,
                                               IReceivementMaterialRepository receivementMaterialRepository,
                                               IReceivementAttachmentRepository receivementAttachmentRepository,
                                               IReceivementProviderReasonRepository receivementProviderReasonRepository
        )
        {
            this.receivementInvoiceOrderRepository = receivementInvoiceOrderRepository;
            this.receivementEmailRepository = receivementEmailRepository;
            this.receivementFileRepository = receivementFileRepository;
            this.receivementService = receivementService;
            this.receivementMaterialRepository = receivementMaterialRepository;
            this.receivementAttachmentRepository = receivementAttachmentRepository;
            this.receivementProviderReasonRepository = receivementProviderReasonRepository;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public IActionResult PostReceivementThirdMaterial([FromForm]ReceivementThirdPartyMaterialRequest request)
        {
            if (ModelState.IsValid)
            {
                if (request != default(ReceivementThirdPartyMaterialRequest))
                {
                    ReceivementInvoiceOrderProjection projection = Mapper.Map<ReceivementInvoiceOrderProjection>(request);
                    ReceivementInvoiceOrder newReceivement = Mapper.Map<ReceivementInvoiceOrder>(request);

                    if (!this.receivementService.ValidateReceivementDate(newReceivement))
                        return BadRequest(new BaseResponse().Error<BaseResponse>("A data de emissão da nota fiscal não pode ser posterior à data de recebimento."));

                    newReceivement.ReceivementsMaterials = this.receivementMaterialRepository.NewReceivementMaterialWithoutOrder(request.MaterialWithoutOrder).ToList();

                    newReceivement.ReceivementProviderReasons = this.receivementProviderReasonRepository.AddReceivementProviderReason(request.ProviderId, null);

                    if (request.Emails != null)
                    {
                        int type = TypeEmails.isReceivementEmail.GetHashCode();
                        newReceivement.ReceivementEmails = this.receivementEmailRepository.AddEmail(request.Emails, type).Cast<ReceivementEmail>().ToList();
                    }

                    if (request.Attachments != null)
                    {
                        ReceivementAttachment attachment = new ReceivementAttachment();
                        newReceivement.ReceivementAttachments = this.receivementFileRepository.UploadFile(request.Attachments, attachment).Cast<ReceivementAttachment>().ToList();
                    }

                    if (request.Photos != null)
                    {
                        ReceivementPhoto photo = new ReceivementPhoto();
                        newReceivement.ReceivementPhotos = this.receivementFileRepository.UploadFile(request.Photos, photo).Cast<ReceivementPhoto>().ToList();
                        if (newReceivement.ReceivementPhotos.Any(x => x == default(ReceivementPhoto)))
                            return BadRequest(new BaseResponse().Error<BaseResponse>("Somente é permitido arquivos .JPG e/ou .PNG para upload de fotos"));
                    }

                    this.receivementInvoiceOrderRepository.Add(newReceivement);
                    this.receivementInvoiceOrderRepository.SaveChanges();

                    return Ok(newReceivement.Id.AsSuccessGenericResponse().Modified<BaseResponse>("Registro finalizado com sucesso"));
                }
            }
            return BadRequest(new BaseResponse().Error<BaseResponse>("Não foi possível finalizar o registro, verifique todos os dados"));
        }

        [HttpPost("AsDraft")]
        [Consumes("multipart/form-data")]
        public IActionResult PostReceivementThirdMaterialDraft([FromForm]ReceivementThirdPartyMaterialAsDraftRequest request)
        {
            if (ModelState.IsValid)
            {
                if (request != default(ReceivementThirdPartyMaterialAsDraftRequest))
                {
                    ReceivementInvoiceOrderProjection projection = Mapper.Map<ReceivementInvoiceOrderProjection>(request);
                    ReceivementInvoiceOrder newReceivement = Mapper.Map<ReceivementInvoiceOrder>(request);

                    if (!this.receivementService.ValidateReceivementDate(newReceivement))
                        return BadRequest(new BaseResponse().Error<BaseResponse>("A data de emissão da nota fiscal não pode ser posterior à data de recebimento."));

                    newReceivement.ReceivementsMaterials = this.receivementMaterialRepository.NewReceivementMaterialWithoutOrder(request.MaterialWithoutOrder).ToList();

                    newReceivement.ReceivementProviderReasons = this.receivementProviderReasonRepository.AddReceivementProviderReason(request.ProviderId, null);

                    if (request.Emails != null)
                    {
                        int type = TypeEmails.isReceivementEmail.GetHashCode();
                        newReceivement.ReceivementEmails = this.receivementEmailRepository.AddEmail(request.Emails, type).Cast<ReceivementEmail>().ToList();
                    }

                    if (request.Attachments != null)
                    {
                        ReceivementAttachment attachment = new ReceivementAttachment();
                        newReceivement.ReceivementAttachments = this.receivementFileRepository.UploadFile(request.Attachments, attachment).Cast<ReceivementAttachment>().ToList();
                    }

                    if (request.Photos != null)
                    {
                        ReceivementPhoto photo = new ReceivementPhoto();
                        newReceivement.ReceivementPhotos = this.receivementFileRepository.UploadFile(request.Photos, photo).Cast<ReceivementPhoto>().ToList();
                        if (newReceivement.ReceivementPhotos.Any(x => x == default(ReceivementPhoto)))
                            return BadRequest(new BaseResponse().Error<BaseResponse>("Somente é permitido arquivos .JPG e/ou .PNG para upload de fotos"));
                    }

                    this.receivementInvoiceOrderRepository.Add(newReceivement);
                    this.receivementInvoiceOrderRepository.SaveChanges();

                    return Ok(newReceivement.Id.AsSuccessGenericResponse().Modified<BaseResponse>("Registro finalizado com sucesso"));
                }
            }
            return BadRequest(new BaseResponse().Error<BaseResponse>("Não foi possível finalizar o registro, verifique todos os dados"));
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public IActionResult PutReceivementThirdMaterial(int id, [FromForm]ReceivementThirdPartyMaterialAsDraftRequest request)
        {
            ReceivementInvoiceOrderProjection projection = Mapper.Map<ReceivementInvoiceOrderProjection>(request);
            ReceivementInvoiceOrder newReceivement = Mapper.Map<ReceivementInvoiceOrder>(request);
            var original = this.receivementService.GetDraft(id);


            if (!string.IsNullOrEmpty(request.MaterialWithoutOrder))
            {
                newReceivement.ReceivementsMaterials = this.receivementMaterialRepository.NewReceivementMaterialWithoutOrder(request.MaterialWithoutOrder).ToList();
                this.receivementMaterialRepository.MergeReceivementsOrder(original.ReceivementsMaterials, newReceivement.ReceivementsMaterials, (orig, other) => Mapper.Map(other, orig));
            }


            if (request.Emails == null)
            {
                if (original.ReceivementEmails.Count != 0)
                {
                    int type = TypeEmails.isReceivementEmail.GetHashCode();
                    newReceivement.ReceivementEmails = this.receivementEmailRepository.AddEmail(request.Emails, type).Cast<ReceivementEmail>().ToList();                
                    newReceivement.ReceivementEmails = new List<ReceivementEmail>();
                }
                this.receivementEmailRepository.MergeReceivementsEmail(original.ReceivementEmails, newReceivement.ReceivementEmails, (orig, other) => Mapper.Map(other, orig));
            }

            if (request.Attachments == null)
            {
                ReceivementAttachment attachment = new ReceivementAttachment();
                newReceivement.ReceivementAttachments = this.receivementFileRepository.UploadFile(request.Attachments, attachment).Cast<ReceivementAttachment>().ToList();
                if (original.ReceivementAttachments.Count != 0)
                {
                    newReceivement.ReceivementAttachments = new List<ReceivementAttachment>();
                }
                this.receivementAttachmentRepository.MergeAttachments(original.ReceivementAttachments, newReceivement.ReceivementAttachments, (orig, other) => Mapper.Map(other, orig));
            }

            if (request.Photos == null)
            {
                ReceivementPhoto photo = new ReceivementPhoto();
                newReceivement.ReceivementPhotos = this.receivementFileRepository.UploadFile(request.Photos, photo).Cast<ReceivementPhoto>().ToList();
                if (newReceivement.ReceivementPhotos.Any(x => x == default(ReceivementPhoto)))
                    return BadRequest(new BaseResponse().Error<BaseResponse>("Somente é permitido arquivos .JPG e/ou .PNG para upload de fotos"));

                if (original.ReceivementPhotos.Count != 0)
                {
                    newReceivement.ReceivementPhotos = new List<ReceivementPhoto>();
                }

                this.receivementFileRepository.MergePhotos(original.ReceivementPhotos, newReceivement.ReceivementPhotos, (orig, other) => Mapper.Map(other, orig));
            }

            newReceivement.Id = original.Id;
            Mapper.Map(newReceivement, original);
            this.receivementInvoiceOrderRepository.Update(original);
            return Ok(id.AsSuccessGenericResponse().Modified<BaseResponse>("Registro finalizado com sucesso"));
        }

        [HttpPut("AsDraft/{id}")]
        [Consumes("multipart/form-data")]
        public IActionResult PutReceivementThirdMaterialAsDraft(int id, [FromForm]ReceivementThirdPartyMaterialRequest request)
        {
            ReceivementInvoiceOrderProjection projection = Mapper.Map<ReceivementInvoiceOrderProjection>(request);
            ReceivementInvoiceOrder newReceivement = Mapper.Map<ReceivementInvoiceOrder>(request);
            var original = this.receivementService.GetDraft(id);


            if (!string.IsNullOrEmpty(request.MaterialWithoutOrder))
            {
                newReceivement.ReceivementsMaterials = this.receivementMaterialRepository.NewReceivementMaterialWithoutOrder(request.MaterialWithoutOrder).ToList();
                this.receivementMaterialRepository.MergeReceivementsOrder(original.ReceivementsMaterials, newReceivement.ReceivementsMaterials, (orig, other) => Mapper.Map(other, orig));
            }


            if (request.Emails == null)
            {
                if (original.ReceivementEmails.Count != 0)
                {
                    int type = TypeEmails.isReceivementEmail.GetHashCode();
                    newReceivement.ReceivementEmails = this.receivementEmailRepository.AddEmail(request.Emails, type).Cast<ReceivementEmail>().ToList();
                    newReceivement.ReceivementEmails = new List<ReceivementEmail>();
                }
                this.receivementEmailRepository.MergeReceivementsEmail(original.ReceivementEmails, newReceivement.ReceivementEmails, (orig, other) => Mapper.Map(other, orig));
            }

            if (request.Attachments == null)
            {
                ReceivementAttachment attachment = new ReceivementAttachment();
                newReceivement.ReceivementAttachments = this.receivementFileRepository.UploadFile(request.Attachments, attachment).Cast<ReceivementAttachment>().ToList();
                if (original.ReceivementAttachments.Count != 0)
                {
                    newReceivement.ReceivementAttachments = new List<ReceivementAttachment>();
                }
                this.receivementAttachmentRepository.MergeAttachments(original.ReceivementAttachments, newReceivement.ReceivementAttachments, (orig, other) => Mapper.Map(other, orig));
            }

            if (request.Photos == null)
            {
                ReceivementPhoto photo = new ReceivementPhoto();
                newReceivement.ReceivementPhotos = this.receivementFileRepository.UploadFile(request.Photos, photo).Cast<ReceivementPhoto>().ToList();
                if (newReceivement.ReceivementPhotos.Any(x => x == default(ReceivementPhoto)))
                    return BadRequest(new BaseResponse().Error<BaseResponse>("Somente é permitido arquivos .JPG e/ou .PNG para upload de fotos"));

                if (original.ReceivementPhotos.Count != 0)
                {
                    newReceivement.ReceivementPhotos = new List<ReceivementPhoto>();
                }

                this.receivementFileRepository.MergePhotos(original.ReceivementPhotos, newReceivement.ReceivementPhotos, (orig, other) => Mapper.Map(other, orig));
            }

            newReceivement.Id = original.Id;
            Mapper.Map(newReceivement, original);
            this.receivementInvoiceOrderRepository.Update(original);
            return Ok(id.AsSuccessGenericResponse().Modified<BaseResponse>("Registro finalizado com sucesso"));
        }
    }
}