using Newtonsoft.Json;

namespace SBEISK.SGM.Presentation.API.ViewModels.Provider.Export
{
    public class ProviderExportViewModel
    {
        [JsonProperty(Order = 1, PropertyName = "Código")]
        public int Id { get; set; }

        [JsonProperty(Order = 2, PropertyName = "Nome")]
        public string CompanyName { get; set; }

        [JsonProperty(Order = 3, PropertyName = "CNPJ")]
        public string Cnpj { get; set; }
        
        [JsonProperty(Order = 4, PropertyName = "Logradouro")]
        public string Street { get; set; }

        [JsonProperty(Order = 5, PropertyName = "Número")]
        public string Number { get; set; }

        [JsonProperty(Order = 6, PropertyName = "Complemento")]
        public string Complement { get; set; }

        [JsonProperty(Order = 7, PropertyName = "Bairro")]
        public string Neighborhood { get; set; }

        [JsonProperty(Order = 8, PropertyName = "Cidade")]
        public string City { get; set; }
        
        [JsonProperty(Order = 9, PropertyName = "CEP")]
        public string Cep { get; set; }
    }
}
