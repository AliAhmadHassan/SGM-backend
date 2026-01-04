using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Presentation.API.ViewModels.Order;

namespace SBEISK.SGM.Presentation.API.ViewModels.Receivement
{
    public class ReceivementInvoiceOrderViewModel : ReceivementInvoiceOrders
    {
        public List<OrderItemViewModel> OrderItem { get; set; }
    }
}