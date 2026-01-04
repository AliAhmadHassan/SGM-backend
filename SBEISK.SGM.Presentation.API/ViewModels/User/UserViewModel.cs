using System.Collections.Generic;
using SBEISK.SGM.Presentation.API.ViewModels.Address;
using SBEISK.SGM.Presentation.API.ViewModels.Profiles;

namespace SBEISK.SGM.Presentation.API.ViewModels.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public string ActiveText { get => Active ? "Ativo" : "Inativo"; }
        public List<UserResponseViewModel> ProfileInstallation { get; set; }
        public string NameInstallations { get; set; }
    }
}