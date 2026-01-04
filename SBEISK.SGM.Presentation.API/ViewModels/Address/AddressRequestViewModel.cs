
using System;
using System.ComponentModel.DataAnnotations;

namespace SBEISK.SGM.Presentation.API.ViewModels.Address
{
    public class AddressRequestViewModel
    {
        [Required(ErrorMessage = "Campo descrição é obrigatório.", AllowEmptyStrings=false)]
        [StringLength(250, ErrorMessage = "Campo Descrição suporta até 250 caracteres.")]
        public string Description { get; set; }

        [StringLength(25, ErrorMessage = "Campo Numero suporta até 25 caracteres.")]
        public string Number { get; set; }

        [StringLength(250, ErrorMessage = "Campo Bairro suporta até 250 caracteres.")]
        public string Neighborhood { get; set; }
        public string Complement { get; set; }
        public string Reference { get; set; }

        [DataType(DataType.PostalCode)]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Campo cidade é obrigatório.", AllowEmptyStrings=false)]
        [Range(1,  Int32.MaxValue, ErrorMessage = "Campo cidade é obrigatório.")]
        public int CityId { get; set; }
        
        [Required(ErrorMessage = "Campo estado é obrigatório.", AllowEmptyStrings=false)]
        [Range(1,  Int32.MaxValue, ErrorMessage = "Campo estado é obrigatório.")]
        public int UfId { get; set; }

        [StringLength(250, ErrorMessage = "Campo Logradouro suporta até 250 caracteres.")]
        public string PublicPlace { get; set; }
    }
}