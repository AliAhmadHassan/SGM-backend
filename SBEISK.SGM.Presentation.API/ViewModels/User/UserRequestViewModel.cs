using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SBEISK.SGM.Presentation.API.ViewModels.UserProfileInstallation;

namespace SBEISK.SGM.Presentation.API.ViewModels
{
    public class UserRequestViewModel
    {
        [Required(ErrorMessage = "Campo Nome é obrigatório.", AllowEmptyStrings = false)]
        [StringLength(128, ErrorMessage = "Campo Nome suporta até 128 caracteres.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "E-mail é obrigatório.", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage="Informe um e-mail em formato válido!")]
        [StringLength(128, ErrorMessage = "Campo E-mail suporta até 128 caracteres.")]
        public string Email { get; set; }
        public bool Active { get; set; }
        
        [Required(ErrorMessage = "É obrigatório associar instalação e perfil ")]
        public IList<Association> Associations { get; set; }
    }
}