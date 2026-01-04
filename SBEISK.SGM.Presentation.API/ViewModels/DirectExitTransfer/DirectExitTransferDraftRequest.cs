using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace SBEISK.SGM.Presentation.API.ViewModels.DirectExitTransfer
{
    public class DirectExitTransferDraftRequest
    {
        public int? InstallationIdSource { get; set; }
        public int? InstallationIdDestiny { get; set; }
        public string VehiclePlate { get; set; }
        public string DriverName { get; set; }
        public string DriverNumber { get; set; }
        public string MaterialCodesAmounts { get; set; }
        public DateTime? PrevisionDate { get; set; }
        public DateTime? DepartureDate { get; set; }    
        public int? UserIdResponsableDelivery { get; set; }    
        public int? UserIdResponsableWithdraw { get; set; }
        public string LocalReference { get; set; }
        public string DeliveryContact { get; set; }
        public string ContactPhone { get; set; }
        public string Emails { get; set; }
        public string Notes { get; set; }
        public IList<IFormFile> Photos { get; set; }
        public IList<IFormFile> Attachments { get; set; }
    }
}