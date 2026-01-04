using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace SBEISK.SGM.Presentation.API.ViewModels
{
    public class DirectExitReceiverRequest 
    {
        public int ReceiverId { get; set; }
        public string MaterialCodesAmounts { get; set; }
        public int UserDeliveryId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int UserWithdrawId { get; set; }
        public string Emails { get; set; }
        public string Notes { get; set; }
        public int InstallationSourceId { get; set; }
        public IList<IFormFile> Attachments { get; set; }
    }
}