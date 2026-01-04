using SBEISK.SGM.Presentation.API.ViewModels.SelectList;
using System.Collections.Generic;

namespace SBEISK.SGM.Presentation.API.ViewModels.Menu
{
    public class BranchOfficeViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public IList<InstallationsViewModel> Instalations { get; set; }
    }
}
