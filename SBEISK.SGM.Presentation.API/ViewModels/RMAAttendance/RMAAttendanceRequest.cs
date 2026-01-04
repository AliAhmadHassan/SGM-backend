using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SBEISK.SGM.Presentation.API.ViewModels.RMAAttendance
{
    public class RMAAttendanceRequest
    {
        public int RMAId { get; set; }

        [Required(ErrorMessage = "Campo Data Entrega não preenchido. Preencha todos os campos")]
        public DateTime DeliverAt { get; set; }

        [Required(ErrorMessage = "Campo Responsável pela Retirada não preenchido. Preencha todos os campos")]
        public int ReceiverUserId { get; set; }

        [Required(ErrorMessage = "Campo Quantidade a Atender não preenchido. Preencha todos os campos")]
        public List<decimal> ReceivementAmount { get; set; }

        [Required(ErrorMessage = "Campo Responsável pela Entrega não preenchido. Preencha todos os campos")]
        public int DeliverUserId { get; set; }
        public string Comments { get; set; }
        public string Emails { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}