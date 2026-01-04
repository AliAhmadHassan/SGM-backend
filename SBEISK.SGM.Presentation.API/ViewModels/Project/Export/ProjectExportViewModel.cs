using Newtonsoft.Json;

namespace SBEISK.SGM.Presentation.API.ViewModels.Provider.Export
{
    public class ProjectExportViewModel
    {
        [JsonProperty(Order = 1, PropertyName = "Código")]
        public int Id { get; set; }

        [JsonProperty(Order = 2, PropertyName = "Descrição")]
        public string Description { get; set; }
        
        [JsonProperty(Order = 3, PropertyName = "Sigla")]
        public string Initials { get; set; }

        [JsonProperty(Order = 4, PropertyName = "Ativo")]
        public string Active { get; set; }

        [JsonProperty(Order = 5, PropertyName = "Filial")]
        public string BranchOffice { get; set; }
    }
}
