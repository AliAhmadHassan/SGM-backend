using Newtonsoft.Json;

namespace SBEISK.SGM.Presentation.API.ViewModels.Provider.Export
{
    public class ReceiverExportViewModel
    {
        [JsonProperty(Order = 1, PropertyName = "Código")]
        public int Code { get; set; }
        [JsonProperty(Order = 2, PropertyName = "Nome")]
        public string Description { get; set; }

        [JsonProperty(Order = 3, PropertyName = "Endereço")]
        public string Address { get; set; }
        
        [JsonProperty(Order = 4, PropertyName = "Tipo de Destinatário")]
        public string ReceiverType { get; set; }
    }
}
