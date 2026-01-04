
using SBEISK.SGM.Presentation.API.ViewModels.Address;
using SBEISK.SGM.Presentation.API.ViewModels.BranchOffice;

namespace SBEISK.SGM.Presentation.API.ViewModels.Project
{
    public class ProjectViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Initials { get; set; }
        public bool Active { get; set; }
        public string ActiveText { get => this.Active ? "Sim" : "Não"; }
        public BranchOfficeResponseViewModel BranchOffice { get; set; }
    }
}