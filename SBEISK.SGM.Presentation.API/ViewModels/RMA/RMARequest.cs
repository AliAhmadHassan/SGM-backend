using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SBEISK.SGM.Presentation.API.ViewModels.RMA
{
    public class RMARequest
    {
        [Required(ErrorMessage = "Campo Instalação de Saída não preenchido. Preencha todos os campos")]
        public int InstallationId { get; set; }

        [Required(ErrorMessage = "Campo Responsável pela Retirada não preenchido. Preencha todos os campos")]
        public string ResponsableId { get; set; }

        [Required(ErrorMessage = "Campo Destinatário não preenchido. Preencha todos os campos")]
        public int ReceiverId { get; set; }
        public string Comments { get; set; }
        public List<IFormFile> Files { get; set; }
        public string Materials { get; set; }
    }
}