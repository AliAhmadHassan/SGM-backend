using Microsoft.AspNetCore.Http;

namespace SBEISK.SGM.Presentation.API.ViewModels.ReceivementInvoiceOrder
{
    public class ReceivementAttachmentRequest
    {
        public int ReceivementInvoiceOrderId { get; set; }
        public IFormFile Document { get; set; }
    }
}