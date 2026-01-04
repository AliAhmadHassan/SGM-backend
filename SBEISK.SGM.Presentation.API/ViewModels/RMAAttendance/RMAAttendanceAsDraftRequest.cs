using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace SBEISK.SGM.Presentation.API.ViewModels.RMAAttendance
{
    public class RMAAttendanceAsDraftRequest
    {
        public int RMAId { get; set; }
        public DateTime? DeliverAt { get; set; }
        public int? ReceiverUserId { get; set; }
        public List<decimal> ReceivementAmount { get; set; }
        public int? DeliverUserId { get; set; }
        public string Comments { get; set; }
        public string Emails { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}