using System;

namespace SBEISK.SGM.Domain.Queries.ReceivementInvoiceOrderQuery
{
    public class ReceivementInvoiceOrderQuery
    {
        public string BranchOffice { get; set; }
        public int? Order { get; set; }
        public string Provider { get; set; }
        public string Status { get; set; }
        public DateTime? DateBegin { get; set; }
        public DateTime? DateFinish { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
    }
}