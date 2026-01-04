using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SBEISK.SGM.Presentation.API.ViewModels.ReceivementInvoiceOrder
{
    public class ReceivementInvoiceWithoutOrderRequest
    {
        [Required(ErrorMessage = "Campo Nota Fiscal não preenchido. Preencha todos os campos")]
        public int Invoice { get; set; }

        [Required(ErrorMessage = "Campo Data Nota Fiscal não preenchido. Preencha todos os campos")]
        public DateTime InvoiceDate { get; set; }
        public string Complement { get; set; }

        [Required(ErrorMessage = "Campo Responsável não preenchido. Preencha todos os campos")]
        public int ReceiverUser { get; set; }

        [Required(ErrorMessage = "Campo Data Recebimento não preenchido. Preencha todos os campos")]
        public DateTime? ReceivementDate { get; set; }

        [Required(ErrorMessage = "Campo Placa do Veículo não preenchido. Preencha todos os campos")]
        public string VehiclePlate { get; set; }

        [Required(ErrorMessage = "Campo Nome do Motorista não preenchido. Preencha todos os campos")]
        public string DriverName { get; set; }

        [Required(ErrorMessage = "Campo Telefone do Motorista não preenchido. Preencha todos os campos")]
        public string DriverNumber { get; set; }
        public int InstallationId { get; set; }

        [Required(ErrorMessage = "Campo Emails não preenchido. Preencha todos os campos")]
        public string Emails { get; set; }
        public string Comments { get; set; }
        public List<IFormFile> Photos { get; set; }
        public List<IFormFile> Attachments { get; set; }

        [Required(ErrorMessage = "Não foi possível localizar o tipo fiscal do recebimento")]
        public int? InvoiceTypeId { get; set; }
        
        [Required(ErrorMessage = "Campo fornecedor não foi preenchido")]
        public int ProviderId { get; set; }

        [Required(ErrorMessage = "Campo motivo não foi preenchido")]
        public int ReasonWithoutOrderId { get; set; }
        public string MaterialWithoutOrder { get; set; }
    }
}