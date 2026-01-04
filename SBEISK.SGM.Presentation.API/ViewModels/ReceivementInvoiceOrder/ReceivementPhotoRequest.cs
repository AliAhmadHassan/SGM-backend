using Microsoft.AspNetCore.Http;

namespace SBEISK.SGM.Presentation.API.ViewModels.ReceivementInvoiceOrder
{
    public class ReceivementPhotoRequest
    {
        public int ReceivementInvoiceOrderId { get; set; }
        public IFormFile Photo { get; set; }
    }
}