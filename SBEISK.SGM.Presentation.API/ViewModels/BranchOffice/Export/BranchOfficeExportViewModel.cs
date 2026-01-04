using Newtonsoft.Json;

namespace SBEISK.SGM.Presentation.API.ViewModels.BranchOffice.Export
{
    public class BranchOfficeExportViewModel
    {
        [JsonProperty(Order = 1, PropertyName = "Código")]
        public int Id { get; set; }
        [JsonProperty(Order = 2, PropertyName = "Nome Fantasia")]
        public string FantasyName { get; set; }
        [JsonProperty(Order = 3, PropertyName = "Nome")]
        public string Name { get; set; }
        [JsonProperty(Order = 4, PropertyName = "Logradouro")]
        public string Street { get; set; }
        [JsonProperty(Order = 5, PropertyName = "Número")]
        public string Number { get; set; }
        [JsonProperty(Order = 6, PropertyName = "Bairro")]
        public string Neighborhood { get; set; }
        [JsonProperty(Order = 7, PropertyName = "CEP")]
        public string Cep { get; set; }
        [JsonProperty(Order = 8, PropertyName = "Cidade")]
        public string City { get; set; }
        [JsonProperty(Order = 10, PropertyName = "Estado")]
        public string Uf { get; set; }
    }
}
