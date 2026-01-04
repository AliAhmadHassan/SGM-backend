using System.Collections.Generic;
using SBEISK.SGM.Presentation.API.ViewModels.Action;

namespace SBEISK.SGM.Presentation.API.ViewModels.Profiles
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }
        public string Description{ get; set; }
        public string Name { get; set; }
        public IList<int> Permissions { get; set; }
    }
}