using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class ReceivementEmail : Entity
    {
        public int ReceivementInvoiceOrderId { get; set; }
        public string Email { get; set; }
        public ReceivementInvoiceOrder ReceivementInvoiceOrder { get; set; }
    }
}