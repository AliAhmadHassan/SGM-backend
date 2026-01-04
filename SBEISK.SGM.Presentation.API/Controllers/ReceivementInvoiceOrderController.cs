using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Permissions;
using SBEISK.SGM.Domain.Projections.Receivement;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.ReceivementInvoiceOrderQuery;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Domain.Repositories.Queries;
using SBEISK.SGM.Domain.Services;
using SBEISK.SGM.Domain.TypeEmails;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.Order;
using SBEISK.SGM.Presentation.API.ViewModels.Receivement;
using SBEISK.SGM.Presentation.API.ViewModels.ReceivementInvoiceOrder;
using SBEISK.SGM.Presentation.API.ViewModels.Response;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ActionAuthorize(ActionPermissions.RecebimentoMaterialNotaEPedido)]
    public class ReceivementInvoiceOrdercontroller : BaseController
    {
        private readonly IReceivementInvoiceOrderRepository receivementInvoiceOrderRepository;
        private readonly IReceivementInvoiceOrderReadOnlyRepository receivementInvoiceOrderReadOnlyRepository;
        private readonly IReceivementFileRepository receivementFileRepository;
        private readonly IReceivementAttachmentRepository receivementAttachmentRepository;
        private readonly IReceivementEmailRepository receivementEmailRepository;
        private readonly IUserRepository userRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IReceivementMaterialRepository receivementMaterialRepository;

        private readonly IReceivementService receivementService;

        public ReceivementInvoiceOrdercontroller(IReceivementInvoiceOrderRepository receivementInvoiceOrderRepository, IReceivementInvoiceOrderReadOnlyRepository receivementInvoiceOrderReadOnlyRepository,
                                                IReceivementFileRepository receivementFileRepository, IReceivementAttachmentRepository receivementAttachmentRepository,
                                                IReceivementEmailRepository receivementEmailRepository,
                                                IUserRepository userRepository, IOrderRepository orderRepository,
                                                IReceivementMaterialRepository receivementMaterialRepository, IReceivementService receivementService)
        {
            this.receivementInvoiceOrderRepository = receivementInvoiceOrderRepository;
            this.receivementInvoiceOrderReadOnlyRepository = receivementInvoiceOrderReadOnlyRepository;
            this.receivementFileRepository = receivementFileRepository;
            this.receivementAttachmentRepository = receivementAttachmentRepository;
            this.receivementEmailRepository = receivementEmailRepository;
            this.userRepository = userRepository;
            this.orderRepository = orderRepository;
            this.receivementMaterialRepository = receivementMaterialRepository;
            this.receivementService = receivementService;
        }

        [HttpGet]
        public IActionResult Index(int page, int items, [FromQuery]ReceivementInvoiceOrderQuery filter)
        {
            GenericPaginatedQuery<ReceivementInvoiceOrderQuery> query = new GenericPaginatedQuery<ReceivementInvoiceOrderQuery>(page, items, filter);
            PaginatedQueryResult<ReceivementInvoiceOrders> receivements = receivementInvoiceOrderReadOnlyRepository.All(query);
            return Ok(receivements.Transform(x => Mapper.Map<ReceivementInvoiceOrders>(x)).AsSuccessGenericResponse());
        }

        [HttpGet("WithItems")]
        public IActionResult WithItems(int id, [FromQuery]List<int?> amounts)
        {
            ReceivementInvoiceOrders receivements = receivementInvoiceOrderReadOnlyRepository.WithItems(id);
            ReceivementInvoiceOrderViewModel receivementsViewModel = Mapper.Map<ReceivementInvoiceOrderViewModel>(receivements);
            receivementsViewModel.OrderItem = Mapper.Map<List<OrderItemViewModel>>(orderRepository.OrderItems(receivementsViewModel.OrderCode, amounts));

            for (int i = 0; i < amounts.Count; i++)
            {
                receivementsViewModel.OrderItem[i].AmountReceived = amounts[i].Value;
            }
            return Ok(receivementsViewModel.AsSuccessGenericResponse());
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public IActionResult PostReceivementInvoiceOrder([FromForm] ReceivementInvoiceOrderRequest request)
        {
            if (ModelState.IsValid)
            {
                if (request != default(ReceivementInvoiceOrderRequest))
                {
                    ReceivementInvoiceOrderProjection projection = Mapper.Map<ReceivementInvoiceOrderProjection>(request);
                    ReceivementInvoiceOrder newReceivement = Mapper.Map<ReceivementInvoiceOrder>(request);

                    if (!this.receivementService.ValidateReceivementDate(newReceivement))
                        return BadRequest(new BaseResponse().Error<BaseResponse>("A data de emissão da nota fiscal não pode ser posterior à data de recebimento."));

                    IList<OrderItem> orderItems = this.orderRepository.OrderItems(request.OrderId, request.ReceivementAmount);
                    newReceivement.ReceivementsMaterials = this.receivementMaterialRepository.NewReceivementMaterial(orderItems, request.ReceivementAmount).ToList();

                    if(request.Emails != null)
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

                    int id = this.receivementService.SaveReceivementInvoiceOrder(projection, newReceivement);
                    this.receivementService.UpdateOrderStatus(orderItems, request.OrderId);
                    return Ok( newReceivement.Id.AsSuccessGenericResponse().Created<BaseResponse>("Registro finalizado com sucesso"));
                }
            }
            return BadRequest(new BaseResponse().Error<BaseResponse>("Não foi possível finalizar o registro, verifique todos os dados"));
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public IActionResult PutReceivementInvoiceOrder(int id, [FromForm]ReceivementInvoiceOrderRequest request)
        {
            ReceivementInvoiceOrderProjection projection = Mapper.Map<ReceivementInvoiceOrderProjection>(request);
            ReceivementInvoiceOrder newReceivement = Mapper.Map<ReceivementInvoiceOrder>(request);
            var original = this.receivementService.GetDraft(id);


            if (request.ReceivementAmount == null || request.ReceivementAmount.Count != 0)
            {
                this.receivementMaterialRepository.MergeReceivementsOrder(original.ReceivementsMaterials, newReceivement.ReceivementsMaterials, (orig, other) => Mapper.Map(other, orig));
            }

            if (original.ReceivementEmails.Count != 0)
            {
                if (request.Emails == null)
                {
                    newReceivement.ReceivementEmails = new List<ReceivementEmail>();
                }
                this.receivementEmailRepository.MergeReceivementsEmail(original.ReceivementEmails, newReceivement.ReceivementEmails, (orig, other) => Mapper.Map(other, orig));
            }

            if (original.ReceivementAttachments.Count != 0)
            {
                if (request.Attachments == null)
                {
                    newReceivement.ReceivementAttachments = new List<ReceivementAttachment>();
                }
                this.receivementAttachmentRepository.MergeAttachments(original.ReceivementAttachments, newReceivement.ReceivementAttachments, (orig, other) => Mapper.Map(other, orig));
            }

            if (original.ReceivementPhotos.Count != 0)
            {
                if (request.Photos == null)
                {
                    newReceivement.ReceivementPhotos = new List<ReceivementPhoto>();
                }

                this.receivementFileRepository.MergePhotos(original.ReceivementPhotos, newReceivement.ReceivementPhotos, (orig, other) => Mapper.Map(other, orig));
            }

            newReceivement.Id = original.Id;
            Mapper.Map(newReceivement, original);
            this.receivementService.SaveReceivementInvoiceOrder(projection, original);
            return Ok(id.AsSuccessGenericResponse().Modified<BaseResponse>("Registro finalizado com sucesso"));
        }

        [HttpPost("AsDraft")]
        [Consumes("multipart/form-data")]
        public IActionResult PostReceivementInvoiceOrderAsDraft([FromForm] ReceivementInvoiceOrderDraftRequest request)
        {
            if (ModelState.IsValid)
            {
                if (request != default(ReceivementInvoiceOrderDraftRequest))
                {
                    ReceivementInvoiceOrderProjection projection = Mapper.Map<ReceivementInvoiceOrderProjection>(request);
                    ReceivementInvoiceOrder newReceivement = Mapper.Map<ReceivementInvoiceOrder>(request);

                    if (!this.receivementService.ValidateReceivementDate(newReceivement))
                        return BadRequest(new BaseResponse().Error<BaseResponse>("A data de emissão da nota fiscal não pode ser posterior à data de recebimento."));

                    IList<OrderItem> orderItems = this.orderRepository.OrderItems(request.OrderId, request.ReceivementAmount);
                    newReceivement.ReceivementsMaterials = this.receivementMaterialRepository.NewReceivementMaterial(orderItems, request.ReceivementAmount).ToList();

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

                    int id = this.receivementService.SaveReceivementInvoiceOrder(projection, newReceivement);
                    this.receivementService.UpdateOrderStatus(orderItems, request.OrderId);
                    
                    return Ok( id.AsSuccessGenericResponse().Success<BaseResponse>("Registro finalizado com sucesso"));
                }
            }
            return BadRequest(new BaseResponse().Error<BaseResponse>("Não foi possível finalizar o registro, verifique todos os dados"));
        }

        [HttpPut("AsDraft/{id}")]
        [Consumes("multipart/form-data")]
        public IActionResult PutReceivementInvoiceOrderAsDraft(int id, [FromForm] ReceivementInvoiceOrderDraftRequest request)
        {
            ReceivementInvoiceOrderProjection projection = Mapper.Map<ReceivementInvoiceOrderProjection>(request);
            ReceivementInvoiceOrder newReceivement = Mapper.Map<ReceivementInvoiceOrder>(request);
            var original = this.receivementService.GetDraft(id);

            if (request.ReceivementAmount == null || request.ReceivementAmount.Count != 0)
            {
                this.receivementMaterialRepository.MergeReceivementsOrder(original.ReceivementsMaterials, newReceivement.ReceivementsMaterials, (orig, other) => Mapper.Map(other, orig));
            }

            if (original.ReceivementEmails.Count != 0)
            {
                if (request.Emails == null)
                {
                    newReceivement.ReceivementEmails = new List<ReceivementEmail>();
                }
                this.receivementEmailRepository.MergeReceivementsEmail(original.ReceivementEmails, newReceivement.ReceivementEmails, (orig, other) => Mapper.Map(other, orig));
            }

            if (original.ReceivementAttachments.Count != 0)
            {
                if (request.Attachments == null)
                {
                    newReceivement.ReceivementAttachments = new List<ReceivementAttachment>();
                }
                this.receivementAttachmentRepository.MergeAttachments(original.ReceivementAttachments, newReceivement.ReceivementAttachments, (orig, other) => Mapper.Map(other, orig));
            }

            if (original.ReceivementPhotos.Count != 0)
            {
                if (request.Photos == null)
                {
                    newReceivement.ReceivementPhotos = new List<ReceivementPhoto>();
                }

                this.receivementFileRepository.MergePhotos(original.ReceivementPhotos, newReceivement.ReceivementPhotos, (orig, other) => Mapper.Map(other, orig));
            }

            newReceivement.Id = original.Id;
            Mapper.Map(newReceivement, original);
            this.receivementService.SaveReceivementInvoiceOrder(projection, original);
            return Ok(id.AsSuccessGenericResponse().Modified<BaseResponse>("Registro finalizado com sucesso"));
        }
    }
}
