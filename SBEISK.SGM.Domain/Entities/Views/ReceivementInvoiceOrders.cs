using System;

namespace SBEISK.SGM.Domain.Entities.Views
{
    public class ReceivementInvoiceOrders
    {   public int InstallationId { get; set; }
        public string BranchOfficeDescription { get; set; }
        public int OrderCode { get; set; }
        public string ProviderName { get; set; }
        public string CNPJ { get; set; }
        public DateTime OrderEmission { get; set; }  
        public string OrderStatus { get; set; }
    }
}