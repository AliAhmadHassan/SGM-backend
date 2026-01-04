using System.ComponentModel.DataAnnotations;

namespace SBEISK.SGM.Presentation.API.ViewModels.BranchOffice
{
    public class BranchOfficeViewModel
    {
        public int Id { get; set; }
        public string FantasyName { get; set; }

        [Required(ErrorMessage = "Campo Name é obrigatório.", AllowEmptyStrings=false)]
        [StringLength(15, ErrorMessage = "Campo Name suporta até 15 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo CNPJ é obrigatório.", AllowEmptyStrings=false)]
        [StringLength(15, ErrorMessage = "Campo CNPJ suporta até 15 caracteres.")]
        public string Cnpj { get; set; }
        
        [StringLength(100, ErrorMessage = "Campo Logradouro suporta até 100 caracteres.")]
        public string Street { get; set; }

        [StringLength(8, ErrorMessage = "Campo Numero suporta até 8 caracteres.")]
        public string Number { get; set; }

        [StringLength(60, ErrorMessage = "Campo Complemento suporta até 60 caracteres.")]
        public string Complement { get; set; }

        [StringLength(80, ErrorMessage = "Campo Bairro suporta até 80 caracteres.")]
        public string Neighborhood { get; set; }

        [StringLength(32, ErrorMessage = "Campo Cidade suporta até 32 caracteres.")]
        public string City { get; set; }

        [DataType(DataType.PostalCode)]
        public string Cep { get; set; }

        [StringLength(2, ErrorMessage = "Campo UF suporta até 2 caracteres.")]
        public string Uf { get; set; }
    }
}
