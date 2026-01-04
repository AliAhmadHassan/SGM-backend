
using System.ComponentModel.DataAnnotations;

namespace SBEISK.SGM.Presentation.API.ViewModels.Project
{
    public class ReceiverRequestViewModel
    {
        [Required(ErrorMessage = "Campo Descrição é obrigatório.", AllowEmptyStrings = false)]
        [StringLength(250, ErrorMessage = "Campo Descrição suporta até 250 caracteres.")]
        public string Description { get; set; }
        [StringLength(250, ErrorMessage = "Campo Endereço suporta até 250 caracteres.")]
        public string Address { get; set; }        
        public int ReceiverTypeId { get; set; }
    }
}