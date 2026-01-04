
using System;
using System.ComponentModel.DataAnnotations;

namespace SBEISK.SGM.Presentation.API.ViewModels.Project
{
    public class ProjectRequestViewModel
    {
        [Required(ErrorMessage = "Campo descrição é obrigatório.", AllowEmptyStrings=false)]
        [StringLength(128, MinimumLength = 3, ErrorMessage = "Campo Descrição suporta até 128 caracteres e mínimo de 3 caracteres.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Campo Sigla é obrigatório.", AllowEmptyStrings=false)]
        [StringLength(100, ErrorMessage = "Campo Sigla suporta até 100 caracteres.")]
        public string Initials { get; set; }

        [Required(ErrorMessage = "Campo Ativo é obrigatório.", AllowEmptyStrings=false)]
        public bool Active { get; set; }     
        
        [Range(1,  Int32.MaxValue, ErrorMessage = "Campo Filial é obrigatório.")]
        public int BranchOfficeId { get; set; }
    }
}