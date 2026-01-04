using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SBEISK.SGM.Presentation.API.ViewModels.ReceivementInvoiceOrder
{
    public class ReceivementInvoiceOrderRequest
    {
        [Required(ErrorMessage = "Campo Pedido não preenchido. Preencha todos os campos")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Campo Nota Fiscal não preenchido. Preencha todos os campos")]
        public int Invoice { get; set; }

        [Required(ErrorMessage = "Campo Data de Nota Fiscal não preenchido. Preencha todos os campos")]
        public DateTime InvoiceDate { get; set; }
        public string Complement { get; set; }

        [Required(ErrorMessage = "Campo Responsável não preenchido. Preencha todos os campos")]
        public int ReceiverUser { get; set; }

        [Required(ErrorMessage = "Campo Data de Recebimento não preenchido. Preencha todos os campos")]
        public DateTime ReceivementDate { get; set; }

        [Required(ErrorMessage = "Campo Quantidade de Recebimento não preenchido. Preencha todos os campos")]
        public List<int?> ReceivementAmount { get; set; }

        [Required(ErrorMessage = "Campo Placa do Veículo não preenchido. Preencha todos os campos")]
        public string VehiclePlate { get; set; }

        [Required(ErrorMessage = "Campo Nome do Motorista não preenchido. Preencha todos os campos")]
        public string DriverName { get; set; }

        [Required(ErrorMessage = "Campo Telefone do Motorista não preenchido. Preencha todos os campos")]
        public string DriverTelephone { get; set; }
        public string Comments { get; set; }

        // [Required(ErrorMessage = "Campo não preenchido. Preencha todos os campos")]
        [Required(ErrorMessage = "InvoiceTypeId")]
        public int InvoiceTypeId { get; set; }

        public string Emails { get; set; }
        public List<IFormFile> Photos { get; set; }
        public List<IFormFile> Attachments { get; set; }

    }
}