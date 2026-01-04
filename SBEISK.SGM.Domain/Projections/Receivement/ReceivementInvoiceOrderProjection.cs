using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace SBEISK.SGM.Domain.Projections.Receivement
{
    public class ReceivementInvoiceOrderProjection
    {
        public int OrderId { get; set; }
        public int Invoice { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Complement { get; set; }
        public int ReceiverUser { get; set; }
        public DateTime ReceivementDate { get; set; }
        public List<int?> ReceivementAmount { get; set; }
        public string VehiclePlate { get; set; }
        public string DriverName { get; set; }
        public string DriverTelephone { get; set; }
        public string Comments { get; set; }
        public bool IsDraft { get; set; }
        public int InvoiceTypeId { get; set; }
        public string Emails { get; set; }
        public List<IFormFile> Photos { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}