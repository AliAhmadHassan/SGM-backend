using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Projections;
using SBEISK.SGM.Domain.Projections.Receivement;

namespace SBEISK.SGM.Domain.Services
{
    public interface IReceivementService
    {
        int SaveReceivementInvoiceOrder(ReceivementInvoiceOrderProjection request,  ReceivementInvoiceOrder newReceivement);

        ReceivementInvoiceOrder GetDraft(int id);
        ReceivementInvoiceOrder GetDraft(ReceivementInvoiceWithoutOrderProjection request, int id);
        int SaveReceivementInvoiceWithoutOrder(ReceivementInvoiceWithoutOrderProjection projection, ReceivementInvoiceOrder receivement);
        bool ValidateReceivementDate(ReceivementInvoiceOrder receivement);
        void UpdateOrderStatus(IList<OrderItem> orderItems, int orderId);
    }
}