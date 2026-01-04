using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class ReceivementAttachment : Entity
    {
        public byte[] Document { get; set; }
        public int ReceivementInvoiceOrderId { get; set; }
        public ReceivementInvoiceOrder ReceivementInvoiceOrder { get; set; }
    }
}