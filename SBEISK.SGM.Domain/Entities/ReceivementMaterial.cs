using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class ReceivementMaterial : Entity
    {
        public decimal? Amount { get; set; }
        public int ReceivementInvoiceOrderId { get; set; }
        public int MaterialId { get; set; }
        public int? OrderItemId { get; set; }
        public ReceivementInvoiceOrder ReceivementInvoiceOrder { get; set; }
        public Material Material { get; set; }
        public OrderItem OrderItem { get; set; }
    }
}