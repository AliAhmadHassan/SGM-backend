using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SBEISK.SGM.Presentation.API.ViewModels.ReceivementInvoiceOrder
{
    public class ReceivementThirdPartyMaterialAsDraftRequest
    {
        public int ProviderId { get; set; }
        public string Complement { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string DocumentNumber { get; set; }
        public int InstallationId { get; set; }
        public int? ReceiverUser { get; set; }
        public DateTime? ReceivementDate { get; set; }
        public string VehiclePlate { get; set; }
        public string DriverName { get; set; }
        public string DriverTelephone { get; set; }
        public string Comments { get; set; }
        public int? InvoiceTypeId { get; set; }
        public string Emails { get; set; }
        public List<IFormFile> Photos { get; set; }
        public string MaterialWithoutOrder { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}