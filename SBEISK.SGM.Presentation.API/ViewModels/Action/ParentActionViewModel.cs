
using System.Collections.Generic;

namespace SBEISK.SGM.Presentation.API.ViewModels.Action
{
    public class ParentActionViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public IList<ActionResponseViewModel> Actions { get; set;}
    }
}