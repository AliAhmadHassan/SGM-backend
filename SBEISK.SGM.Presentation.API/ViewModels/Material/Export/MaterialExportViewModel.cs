using Newtonsoft.Json;

namespace SBEISK.SGM.Presentation.API.ViewModels.Material.Export
{
    public class MaterialExportViewModel
    {
        [JsonProperty(Order = 1, PropertyName = "Código")]
        public string Code { get; set; }

        [JsonProperty(Order = 2, PropertyName = "Descrição")]
        public string Description { get; set; }

        [JsonProperty(Order = 3, PropertyName = "Unidade")]
        public string Unit { get; set; }
    }
}