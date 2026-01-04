using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Projections;
using SBEISK.SGM.Domain.Projections.Receivement;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Domain.Services;
using SBEISK.SGM.Infraestructure.Data.Context;

namespace SBEISK.SGM.Infraestructure.Data.Services
{
    public class ReceivementService : IReceivementService
    {
        private readonly SgmDataContext context;
        private readonly IReceivementInvoiceOrderRepository receivementInvoiceOrderRepository;
        private readonly IReceivementFileRepository receivementFileRepository;
        private readonly IReceivementAttachmentRepository receivementAttachmentRepository;
        private readonly IReceivementEmailRepository receivementEmailRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IReceivementMaterialRepository receivementMaterialRepository;
        private readonly int PartiallyReceived = 4;
        private readonly int Received = 3;
        private readonly IReceivementInvoiceWithoutOrderRepository receivementInvoiceWithoutOrderRepository;
        public ReceivementService(SgmDataContext context, IReceivementInvoiceOrderRepository receivementInvoiceOrderRepository,
                                               IReceivementFileRepository receivementFileRepository, IReceivementAttachmentRepository receivementAttachmentRepository,
                                               IReceivementEmailRepository receivementEmailRepository, IOrderRepository orderRepository,
                                               IReceivementMaterialRepository receivementMaterialRepository,
                                               IReceivementInvoiceWithoutOrderRepository receivementInvoiceWithoutOrderRepository)
        {
            this.context = context;
            this.receivementInvoiceOrderRepository = receivementInvoiceOrderRepository;
            this.receivementFileRepository = receivementFileRepository;
            this.receivementAttachmentRepository = receivementAttachmentRepository;
            this.receivementEmailRepository = receivementEmailRepository;
            this.orderRepository = orderRepository;
            this.receivementMaterialRepository = receivementMaterialRepository;
            this.receivementInvoiceWithoutOrderRepository = receivementInvoiceWithoutOrderRepository;
        }

        public int SaveReceivementInvoiceOrder(ReceivementInvoiceOrderProjection request, ReceivementInvoiceOrder newReceivement)
        {
            newReceivement.InstallationId = this.orderRepository.Find(request.OrderId).InstalationId;

            if (newReceivement.Id == 0)
            {
                this.receivementInvoiceOrderRepository.Add(newReceivement);
            }
            else
            {
                this.receivementInvoiceOrderRepository.Update(newReceivement);
            }

            this.receivementInvoiceOrderRepository.SaveChanges();

            return newReceivement.Id;
        }

        public ReceivementInvoiceOrder GetDraft(int id)
        {
            var original = this.receivementInvoiceOrderRepository.Query().Include(x => x.ReceivementEmails)
                                                                         .Include(x => x.ReceivementAttachments)
                                                                         .Include(x => x.ReceivementPhotos)
                                                                         .Include(x => x.ReceivementsMaterials)
            .FirstOrDefault(x => x.Id == id);
            return original;
        }

        public ReceivementInvoiceOrder GetDraft(ReceivementInvoiceWithoutOrderProjection request, int receivementId)
        {
            ReceivementInvoiceOrder projection = this.receivementInvoiceOrderRepository.Query().FirstOrDefault(x => x.Id.Equals(receivementId) && x.IsDraft);
            return projection;
        }

        public int SaveReceivementInvoiceWithoutOrder(ReceivementInvoiceWithoutOrderProjection projection, ReceivementInvoiceOrder receivement)
        {

            if (receivement.Id == 0)
            {
                this.receivementInvoiceOrderRepository.Add(receivement);
            }
            else
            {
                this.receivementInvoiceOrderRepository.Update(receivement);
            }

            this.receivementInvoiceOrderRepository.SaveChanges();

            return receivement.Id;
        }

        public int SaveThirdPartyMaterialReceivement(ReceivementInvoiceWithoutOrderProjection projection, ReceivementInvoiceOrder receivement)
        {
            if (receivement.Id == 0)
            {
                this.receivementInvoiceOrderRepository.Add(receivement);
            }
            else
            {
                this.receivementInvoiceOrderRepository.Update(receivement);
            }

            this.receivementInvoiceOrderRepository.SaveChanges();

            return receivement.Id;
        }

        public bool ValidateReceivementDate(ReceivementInvoiceOrder receivement)
        {
            if (receivement.InvoiceCreatedAt > receivement.ReceivementDate)
                return false;
            return true;
        }

        public void UpdateOrderStatus(IList<OrderItem> orderItems, int orderId)
        {
            var order = this.context.Orders.Find(orderId);

            foreach (var item in orderItems)
            {
                if (item.Quantity != item.AmountReceived)
                {
                    order.OrderStatusId = PartiallyReceived;
                    return;
                }
            }

            order.OrderStatusId = Received;
        }
    }
}