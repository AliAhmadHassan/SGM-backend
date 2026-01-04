using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace SBEISK.SGM.Presentation.API.ViewModels.Transfer
{
    public class TransferRequestDraftViewModel
    {
        public int STMId { get; set; }
        public int? UserReceiverId { get; set; }
        public DateTime? ReceivementDate { get; set; }
        public int? InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string VehiclePlate { get; set; }
        public string DriverName { get; set; }
        public string DriverNumber { get; set; }
        public string Emails { get; set; }
        public string Notes { get; set; }
        public int? TransferStatusId { get; set; }
        public IList<IFormFile> Attachments { get; set; }
        public IList<IFormFile> Photos { get; set; }
    }
}