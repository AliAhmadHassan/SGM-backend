using Microsoft.AspNetCore.Http;

namespace SBEISK.SGM.Presentation.API.ViewModels.Divergence
{
    public class DivergenceFileRequestViewModel
    {
        public int DivergenceId { get; set; }
        public IFormFile File { get; set; }
        public string Name { get; set; }
    }
}