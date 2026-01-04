using Newtonsoft.Json;

namespace SBEISK.SGM.Presentation.API.ViewModels.Address.Export
{
    public class UserExportViewModel
    {
        [JsonProperty(Order = 1, PropertyName = "Usuário")]
        public string Name { get; set; }

        [JsonProperty(Order = 2, PropertyName = "Perfil")]
        public string Profile { get; set; }

        [JsonProperty(Order = 3, PropertyName = "Instalações")]
        public string NameInstallations { get; set; }
        
        [JsonProperty(Order = 4, PropertyName = "Status")]
        public string Active { get; set; }
    }
}
