using SBEISK.SGM.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace SBEISK.SGM.Domain.Entities
{
    public class Order : Entity, ISoftDelete
    {
        public DateTime Emission { get; set; }
        public int ProviderId { get; set; }
        public int InstalationId { get; set; }
        public int OrderStatusId { get; set; }
        public int SbeiCode  { get; set; }
        public DateTime? DeletedAt { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public ICollection<ReceivementInvoiceOrder> ReceivementInvoiceOrders { get; set; }
    }
}
