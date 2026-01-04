using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace SBEISK.SGM.Domain.Projections.Receivement
{
    public class ReceivementInvoiceWithoutOrderProjection
    {
        public int Invoice { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string Complement { get; set; }
        public int? ReceiverUser { get; set; }
        public DateTime? ReceivementDate { get; set; }
        public string VehiclePlate { get; set; }
        public string DriverName { get; set; }
        public string DriverNumber { get; set; }
        public int? InstallationId { get; set; }
        public string Emails { get; set; }
        public string Comments { get; set; }
        public List<IFormFile> Photos { get; set; }
        public List<IFormFile> Attachments { get; set; }
        public bool IsDraft { get; set; }
        public int? InvoiceTypeId { get; set; }
        public int ReasonWithoutOrderId { get; set; }
        public int ProviderId { get; set; }
        public string MaterialWithoutOrder { get; set; }
    }
}