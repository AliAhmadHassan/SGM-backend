using Microsoft.AspNetCore.Http;

namespace SBEISK.SGM.Presentation.API.ViewModels.FileExample
{
    public class FileExampleViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public IFormFile File { get; set; }
        // Another domain properties here.
    }
}
