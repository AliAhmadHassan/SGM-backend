using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace SBEISK.SGM.Presentation.API.ViewModels.Receivement
{
    public class DevolutionDraftRequest
    {
        public int? UserResponsableId { get; set; }
        public DateTime? ReceivementDate { get; set; }
        public int? DirectExitReceiverId { get; set; }
        public string ReceivementDevolutionMaterials { get; set; }
        public string Emails { get; set; } 
        public string Notes { get; set; }
        public IList<IFormFile> Attachments { get; set; }
    }
}