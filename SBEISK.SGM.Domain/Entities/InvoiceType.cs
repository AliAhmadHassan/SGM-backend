using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class InvoiceType : Entity
    {
        public string Description { get; set; }
        public ICollection<ReceivementInvoiceOrder> ReceivementInvoiceOrders { get; set; }
    }
}