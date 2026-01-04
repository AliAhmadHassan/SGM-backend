using Newtonsoft.Json;

namespace SBEISK.SGM.Presentation.API.ViewModels.Export.Export
{
    public class InstallationExportViewModel
    {
        [JsonProperty(Order = 1, PropertyName = "Código")]
        public int Id { get; set; }

        [JsonProperty(Order = 2, PropertyName = "Nome")]
        public string Name { get; set; }

        [JsonProperty(Order = 3, PropertyName = "Descrição")]
        public string Description { get; set; }

        [JsonProperty(Order = 4, PropertyName = "Tipo")]
        public string Type { get; set; }
        
        [JsonProperty(Order = 5, PropertyName = "Projeto")]
        public string Project { get; set; }

        [JsonProperty(Order = 6, PropertyName = "Endereço")]
        public string Address { get; set; }

        [JsonProperty(Order = 7, PropertyName = "Perm. Mat. de Terceiros")]
        public string ThirdMaterial { get; set; }
    }
}
