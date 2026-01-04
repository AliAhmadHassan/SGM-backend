using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class ReceivementProviderReason : Entity
    {
        public int ReceivementId { get; set; }
        public int? ProviderId { get; set; }
        public int? ReasonWithoutOrderId { get; set; }
        public ReceivementInvoiceOrder Receivement { get; set; }
        public Provider Provider { get; set; }
        public ReasonWithoutOrder ReasonWithoutOrder { get; set; }
    }
}