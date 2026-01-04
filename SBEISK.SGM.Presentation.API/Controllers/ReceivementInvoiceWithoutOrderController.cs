using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Projections.Receivement;
using SBEISK.SGM.Domain.Projections.Material;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Domain.Services;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.Material;
using SBEISK.SGM.Presentation.API.ViewModels.ReceivementInvoiceOrder;
using SBEISK.SGM.Presentation.API.ViewModels.Response;
using SBEISK.SGM.Domain.TypeEmails;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ActionAuthorize(ActionPermissions.RecebimentoMaterialFornecedorComNotaSemPedido)]
    public class ReceivementInvoiceWithoutOrderController : BaseController
    {
        private readonly IReceivementInvoiceWithoutOrderRepository receivementInvoiceWithoutOrderRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IMaterialRepository materialRepository;
        private readonly IReceivementEmailRepository receivementEmailRepository;
        private readonly IReceivementFileRepository receivementFileRepository;
        private readonly IReceivementMaterialRepository receivementMaterialRepository;
        private readonly IReceivementService receivementService;
        private readonly IUserRepository userRepository;
        private readonly IReceivementAttachmentRepository receivementAttachmentRepository;
        private readonly IReceivementProviderReasonRepository receivementProviderReason;
        public ReceivementInvoiceWithoutOrderController(IReceivementInvoiceWithoutOrderRepository receivementInvoiceWithoutOrderRepository,
                                                        IOrderRepository orderRepository, IMaterialRepository materialRepository,
                                                        IReceivementEmailRepository receivementEmailRepository, IReceivementFileRepository receivementFileRepository,
                                                        IReceivementMaterialRepository receivementMaterialRepository, IReceivementService receivementService,
                                                        IUserRepository userRepository, IReceivementAttachmentRepository receivementAttachmentRepository,
                                                        IReceivementProviderReasonRepository receivementProviderReason)
        {
            this.receivementInvoiceWithoutOrderRepository = receivementInvoiceWithoutOrderRepository;
            this.materialRepository = materialRepository;
            this.orderRepository = orderRepository;
            this.receivementEmailRepository = receivementEmailRepository;
            this.receivementFileRepository = receivementFileRepository;
            this.receivementMaterialRepository = receivementMaterialRepository;
            this.receivementService = receivementService;
            this.userRepository = userRepository;
            this.receivementAttachmentRepository = receivementAttachmentRepository;
            this.receivementProviderReason = receivementProviderReason;
        }

        ///<summary>
        ///Para envio do campo MaterialWithoutOrder, é feito o envio no formato Json, como exemplo: [{"materialCode": "20.369.58799", 
        ///                                                                                           "receivementAmount": 250, 
        ///                                                                                           "unityPrice": 12.90}]
        ///</summary>

        [HttpPost]
        [Consumes("multipart/form-data")]
        public IActionResult PostReceivementInvoiceWithoutOrder([FromForm] ReceivementInvoiceWithoutOrderRequest request)
        {
            if (ModelState.IsValid)
            {
                if (request != default(ReceivementInvoiceWithoutOrderRequest))
                {
                    ReceivementInvoiceWithoutOrderProjection projection = Mapper.Map<ReceivementInvoiceWithoutOrderProjection>(request);
                    ReceivementInvoiceOrder receivementMapped = Mapper.Map<ReceivementInvoiceOrder>(request);

                    if (!this.receivementService.ValidateReceivementDate(receivementMapped))
                        return BadRequest(new BaseResponse().Error<BaseResponse>("A data de emissão da nota fiscal não pode ser posterior à data de recebimento."));

                    receivementMapped.ReceivementsMaterials = this.receivementMaterialRepository.NewReceivementMaterialWithoutOrder(request.MaterialWithoutOrder).ToList();

                    receivementMapped.ReceivementProviderReasons = this.receivementProviderReason.AddReceivementProviderReason(request.ProviderId, request.ReasonWithoutOrderId);

                    ReceivementInvoiceOrder receivementDraft = this.receivementService.GetDraft(projection, receivementMapped.Id);
                    int id = 0;

                    if (request.Emails != null || request.Emails.Length != 0)
                    {
                        int type = TypeEmails.isReceivementEmail.GetHashCode();
                        receivementMapped.ReceivementEmails = this.receivementEmailRepository.AddEmail(request.Emails, type).Cast<ReceivementEmail>().ToList();
                        if (receivementMapped.ReceivementEmails.Any(x => x == default(ReceivementEmail)))
                            return BadRequest(new BaseResponse().Error<BaseResponse>("Um ou mais dos e-mails informados, não estão cadastrados no sistema. Favor contactar um administrador."));
                    }

                    if (request.Attachments != null)
                    {
                        ReceivementAttachment attachment = new ReceivementAttachment();
                        receivementMapped.ReceivementAttachments = this.receivementFileRepository.UploadFile(request.Attachments, attachment).Cast<ReceivementAttachment>().ToList();
                    }

                    if (request.Photos != null)
                    {
                        ReceivementPhoto photo = new ReceivementPhoto();
                        receivementMapped.ReceivementPhotos = this.receivementFileRepository.UploadFile(request.Photos, photo).Cast<ReceivementPhoto>().ToList();
                        if (receivementMapped.ReceivementPhotos.Any(x => x == default(ReceivementPhoto)))
                            return BadRequest(new BaseResponse().Error<BaseResponse>("Somente é permitido arquivos .JPG e/ou .PNG para upload de fotos"));
                    }

                    id = this.receivementService.SaveReceivementInvoiceWithoutOrder(projection, receivementMapped);
                    return Ok(id.AsSuccessGenericResponse().Success<BaseResponse>("Recebimento finalizado com sucesso."));
                }
            }
            return BadRequest(new BaseResponse().Error<BaseResponse>("Não foi possível finalizar o recebimento. Favor revisar os dados."));
        }

        [HttpPost("asDraft")]
        [Consumes("multipart/form-data")]
        public IActionResult PostReceivementInvoiceWithoutOrderDraft([FromForm] ReceivementInvoiceWithoutOrderDraft draft)
        {
            if (ModelState.IsValid)
            {
                if (draft != default(ReceivementInvoiceWithoutOrderDraft))
                {
                    ReceivementInvoiceWithoutOrderProjection projection = Mapper.Map<ReceivementInvoiceWithoutOrderProjection>(draft);
                    ReceivementInvoiceOrder receivementMapped = Mapper.Map<ReceivementInvoiceOrder>(draft);

                    if (!this.receivementService.ValidateReceivementDate(receivementMapped))
                    {
                        return BadRequest(new BaseResponse().Error<BaseResponse>("A data de emissão da nota fiscal não pode ser posterior à data de recebimento."));
                    }

                    receivementMapped.ReceivementsMaterials = this.receivementMaterialRepository.NewReceivementMaterialWithoutOrder(draft.MaterialWithoutOrder).ToList();

                    receivementMapped.ReceivementProviderReasons = this.receivementProviderReason.AddReceivementProviderReason(draft.ProviderId, draft.ReasonWithoutOrderId);

                    ReceivementInvoiceOrder receivementDraft = this.receivementService.GetDraft(projection, receivementMapped.Id);
                    int id = 0;

                    if (draft.Emails != null)
                    {
                        int type = TypeEmails.isReceivementEmail.GetHashCode();
                        receivementMapped.ReceivementEmails = this.receivementEmailRepository.AddEmail(draft.Emails, type).Cast<ReceivementEmail>().ToList();
                        if (receivementMapped.ReceivementEmails.Any(x => x == default(ReceivementEmail)))
                            return BadRequest(new BaseResponse().Error<BaseResponse>("Um ou mais dos e-mails informados, não estão cadastrados no sistema. Favor contactar um administrador."));
                    }

                    if (draft.Attachments != null)
                    {
                        ReceivementAttachment attachment = new ReceivementAttachment();
                        receivementMapped.ReceivementAttachments = this.receivementFileRepository.UploadFile(draft.Attachments, attachment).Cast<ReceivementAttachment>().ToList();
                    }

                    if (draft.Photos != null)
                    {
                        ReceivementPhoto photo = new ReceivementPhoto();
                        receivementMapped.ReceivementPhotos = this.receivementFileRepository.UploadFile(draft.Photos, photo).Cast<ReceivementPhoto>().ToList();
                        if (receivementMapped.ReceivementPhotos.Any(x => x == default(ReceivementPhoto)))
                            return BadRequest(new BaseResponse().Error<BaseResponse>("Somente é permitido arquivos .JPG e/ou .PNG para upload de fotos"));
                    }

                    id = this.receivementService.SaveReceivementInvoiceWithoutOrder(projection, receivementMapped);
                    return Ok(id.AsSuccessGenericResponse().Created<BaseResponse>("Rascunho salvo com sucesso"));
                }
            }
            return BadRequest(new BaseResponse().Error<BaseResponse>("Não foi possível finalizar o recebimento, revise todos os dados inseridos"));
        }

        [HttpPut("{id}")]
        public IActionResult PutReceivementInvoiceWithoutOrder(int id, [FromForm] ReceivementInvoiceWithoutOrderRequest request)
        {
            ReceivementInvoiceWithoutOrderProjection projection = Mapper.Map<ReceivementInvoiceWithoutOrderProjection>(request);
            ReceivementInvoiceOrder receivement = Mapper.Map<ReceivementInvoiceOrder>(request);
            ReceivementInvoiceOrder original = this.receivementService.GetDraft(id);

            if (!string.IsNullOrEmpty(request.MaterialWithoutOrder))
            {
                this.receivementMaterialRepository.MergeReceivementsOrder(original.ReceivementsMaterials, receivement.ReceivementsMaterials, (orig, other) => Mapper.Map(other, orig));
            }

            if (original.ReceivementEmails.Count != 0)
            {
                if (request.Emails == null)
                {
                    receivement.ReceivementEmails = new List<ReceivementEmail>();
                }
                this.receivementEmailRepository.MergeReceivementsEmail(original.ReceivementEmails, receivement.ReceivementEmails, (orig, other) => Mapper.Map(other, orig));
            }

            if (original.ReceivementAttachments.Count != 0)
            {
                if (request.Attachments == null)
                {
                    receivement.ReceivementAttachments = new List<ReceivementAttachment>();
                }
                this.receivementAttachmentRepository.MergeAttachments(original.ReceivementAttachments, receivement.ReceivementAttachments, (orig, other) => Mapper.Map(other, orig));
            }

            if (original.ReceivementPhotos.Count != 0)
            {
                if (request.Photos == null)
                {
                    receivement.ReceivementPhotos = new List<ReceivementPhoto>();
                }
                this.receivementFileRepository.MergePhotos(original.ReceivementPhotos, receivement.ReceivementPhotos, (orig, other) => Mapper.Map(other, orig));
            }

            receivement.Id = original.Id;
            Mapper.Map(receivement, original);
            this.receivementService.SaveReceivementInvoiceWithoutOrder(projection, original);

            return Ok(id.AsSuccessGenericResponse().Modified<BaseResponse>("Recebimento finalizado com sucesso"));
        }

        [HttpPut("asDraft/{id}")]
        public IActionResult PutReceivementInvoiceWithoutOrderAsDraft(int id, [FromForm] ReceivementInvoiceWithoutOrderDraft request)
        {
            ReceivementInvoiceWithoutOrderProjection projection = Mapper.Map<ReceivementInvoiceWithoutOrderProjection>(request);
            ReceivementInvoiceOrder receivement = Mapper.Map<ReceivementInvoiceOrder>(projection);
            ReceivementInvoiceOrder original = this.receivementService.GetDraft(id);

            this.receivementMaterialRepository.MergeReceivementsOrder(original.ReceivementsMaterials, receivement.ReceivementsMaterials, (orig, other) => Mapper.Map(other, orig));

            if (original.ReceivementEmails.Count != 0)
            {
                if (request.Emails == null)
                {
                    receivement.ReceivementEmails = new List<ReceivementEmail>();
                }
                this.receivementEmailRepository.MergeReceivementsEmail(original.ReceivementEmails, receivement.ReceivementEmails, (orig, other) => Mapper.Map(other, orig));
            }

            if (original.ReceivementAttachments.Count != 0)
            {
                if (request.Attachments == null)
                {
                    receivement.ReceivementAttachments = new List<ReceivementAttachment>();
                }
                this.receivementAttachmentRepository.MergeAttachments(original.ReceivementAttachments, receivement.ReceivementAttachments, (orig, other) => Mapper.Map(other, orig));
            }

            if (original.ReceivementPhotos.Count != 0)
            {
                if (request.Photos == null)
                {
                    receivement.ReceivementPhotos = new List<ReceivementPhoto>();
                }
                this.receivementFileRepository.MergePhotos(original.ReceivementPhotos, receivement.ReceivementPhotos, (orig, other) => Mapper.Map(other, orig));
            }

            receivement.Id = original.Id;
            Mapper.Map(receivement, original);
            this.receivementService.SaveReceivementInvoiceWithoutOrder(projection, original);

            return Ok(id.AsSuccessGenericResponse().Modified<BaseResponse>("Recebimento finalizado com sucesso"));
        }

        ///<summary>
        ///Para busca no campo MaterialCodes, é feito o envio no formato Json, como exemplo: [{"materialCode": "20.369.58799", 
        ///                                                                                           "receivementAmount": 250, 
        ///                                                                                           "unityPrice": 12.90}]
        ///</summary>
        [HttpGet]
        [Route("materialWithoutOrder")]
        public IActionResult ReceivementMaterialWithoutOrder(string materialCodes)
        {
            List<MaterialWithoutOrderProjection> materialWithoutOrderProjections = JsonConvert.DeserializeObject<List<MaterialWithoutOrderProjection>>(materialCodes);
            List<WithoutOrder> materials = new List<WithoutOrder>();

            foreach (MaterialWithoutOrderProjection code in materialWithoutOrderProjections)
            {
                Material material = this.materialRepository.MaterialByCode(code.MaterialCode);
                WithoutOrder withoutOrder = Mapper.Map<WithoutOrder>(material);
                withoutOrder.ReceivementAmount = code.ReceivementAmount;
                withoutOrder.UnityPrice = code.UnityPrice;
                withoutOrder.TotalPrice = this.receivementInvoiceWithoutOrderRepository.CalculateTotalPrice(code.ReceivementAmount, code.UnityPrice);

                materials.Add(withoutOrder);
            }
            return Ok(materials.AsSuccessGenericResponse());
        }
    }
}