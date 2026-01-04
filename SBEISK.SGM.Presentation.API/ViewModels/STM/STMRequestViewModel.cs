using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace SBEISK.SGM.Presentation.API.ViewModels.STM
{
    public class STMRequestViewModel
    {
        public int STMStatusId { get; set; }
        public int UserRequestId { get; set; }
        public int InstallationSourceId { get; set; }
        public int InstallationDestinyId { get; set; }
        public int UserWithdrawId { get; set; }
        public string MaterialCodesAmount { get; set; }
        public string Emails { get; set; }
        public string Notes { get; set; }
        public IList<IFormFile> Attachments { get; set; }
    }
}