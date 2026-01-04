using Newtonsoft.Json;

namespace SBEISK.SGM.Presentation.API.ViewModels.Address.Export
{
    public class AddressExportViewModel
    {
        [JsonProperty(Order = 1, PropertyName = "Logradouro")]
        public string PublicPlace { get; set; }

        [JsonProperty(Order = 2, PropertyName = "Descrição")]
        public string Description { get; set; }

        [JsonProperty(Order = 3, PropertyName = "Número")]
        public string Number { get; set; }
        
        [JsonProperty(Order = 4, PropertyName = "Bairro")]
        public string Neighborhood { get; set; }

        [JsonProperty(Order = 5, PropertyName = "Complemento")]
        public string Complement { get; set; }

        [JsonProperty(Order = 7, PropertyName = "Referência")]
        public string Reference { get; set; }

        [JsonProperty(Order = 8, PropertyName = "CEP")]
        public string Cep { get; set; }

        [JsonProperty(Order = 9, PropertyName = "Cidade")]
        public string City { get; set; }

        [JsonProperty(Order = 9, PropertyName = "Estado")]
        public string Uf { get; set; }
    }
}
