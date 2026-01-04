using Newtonsoft.Json;

namespace SBEISK.SGM.Presentation.API.ViewModels.Profiles.Export
{
    public class UserProfileExportViewModel
    {
        [JsonProperty(Order = 1, PropertyName = "Nome")]
        public string ProfileName { get; set; }

        [JsonProperty(Order = 2, PropertyName = "Descrição")]
        public string ProfileDescription { get; set; }

        [JsonProperty(Order = 3, PropertyName = "Permissões")]
         public string Permissions { get; set; }
    }
}

       
       
       