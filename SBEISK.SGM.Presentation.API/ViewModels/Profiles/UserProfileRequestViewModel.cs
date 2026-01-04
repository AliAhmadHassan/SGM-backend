using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SBEISK.SGM.Presentation.API.ViewModels.Profiles
{
    public class UserProfileRequest
    {
        [Required(ErrorMessage = "Campo Nome é obrigatório!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Campo Nome suporta no máximo 100 caracteres e no mínimo 3 caracteres.")]
        public string Name { get; set; }
        
        [StringLength(250)]
        public string Description{ get; set; }
 
        [Required(ErrorMessage = "Não foi atribuida nenhuma permissão")]
        public IList<int> Permissions { get; set; }
    }
}