using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class ReceivementPhoto : Entity
    {
        public byte[] Photo { get; set; }
        public int ReceivementInvoiceOrderId { get; set; }
        public ReceivementInvoiceOrder ReceivementInvoiceOrder { get; set; }
    }
}