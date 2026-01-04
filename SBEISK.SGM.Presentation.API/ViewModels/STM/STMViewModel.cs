using System;

namespace SBEISK.SGM.Presentation.API.ViewModels.STM
{
    public class STMViewModel
    {
        public int STM { get; set; }
        public int Transfer { get; set; }
        public DateTime EmissionDate { get; set; }
        public string Status { get; set; }
        public string Requester { get; set; }
        public string Approver { get; set; }
        public string InstallationSource { get; set; }
        public string InstallationDestiny { get; set; }
    }
}