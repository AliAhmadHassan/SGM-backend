using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SBEISK.SGM.Presentation.API.ViewModels.ReceivementInvoiceOrder
{
    public class ReceivementThirdPartyMaterialRequest
    {
        [Required(ErrorMessage = "Campo Complemento não preenchido. Preencha todos os campos")]
        public int ProviderId { get; set; }

        [Required(ErrorMessage = "Campo Complemento não preenchido. Preencha todos os campos")]
        public string Complement { get; set; }

        [Required(ErrorMessage = "Campo Data Documento não preenchido. Preencha todos os campos")]
        public DateTime? DocumentDate { get; set; }

        [Required(ErrorMessage = "Campo Número Documento não preenchido. Preencha todos os campos")]
        public string DocumentNumber { get; set; }

        [Required(ErrorMessage = "Campo Pedido não preenchido. Preencha todos os campos")]
        public int InstallationId { get; set; }

        [Required(ErrorMessage = "Campo Recebido por não preenchido. Preencha todos os campos")]
        public int ReceiverUser { get; set; }

        [Required(ErrorMessage = "Campo Data Recebimento não preenchido. Preencha todos os campos")]
        public DateTime? ReceivementDate { get; set; }

        [Required(ErrorMessage = "Campo Placa Veículo não preenchido. Preencha todos os campos")]
        public string VehiclePlate { get; set; }

        [Required(ErrorMessage = "Campo Nome do Motorista não preenchido. Preencha todos os campos")]
        public string DriverName { get; set; }

        [Required(ErrorMessage = "Campo Telefone do Motorista não preenchido. Preencha todos os campos")]
        public string DriverTelephone { get; set; }

        [Required(ErrorMessage = "Campo Observações não preenchido. Preencha todos os campos")]
        public string Comments { get; set; }

        [Required(ErrorMessage = "Campo InvoiceTypeId não preenchido. Preencha todos os campos")]
        public int? InvoiceTypeId { get; set; }

        [Required(ErrorMessage = "Campo Incluir Emails não preenchido. Preencha todos os campos")]
        public string Emails { get; set; }

        public List<IFormFile> Photos { get; set; }

        [Required(ErrorMessage = "Dados do Material não preenchidos. Preencha todos os campos")]
        public string MaterialWithoutOrder { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}