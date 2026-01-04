using System.ComponentModel.DataAnnotations;

namespace SBEISK.SGM.Presentation.API.ViewModels.Login
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage="Email em formato inválido!")]
        [Required(ErrorMessage="O campo email é obrigatório!")]
        public string Username { get; set; }
        [Required(ErrorMessage="O campo senha é obrigatório!")]
        public string Password { get; set; }
    }
}
