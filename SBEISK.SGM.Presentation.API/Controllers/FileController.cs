using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Infraestructure.Data.Storage.Base;
using SBEISK.SGM.Presentation.API.Attributes;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using System.IO;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    public class FileController : BaseController
    {
        private readonly IStorage storage;

        public FileController(IStorage storage)
        {
            this.storage = storage;
        }

        [Route("/File/{folderName}/{fileName}")]
        [HttpGet]
        [AllowAnonymous]
        [ValidateJwtToken]
        public IActionResult Get(string token, string folderName, string fileName)
        {
            Stream fileStream = storage.GetFileStream(folderName, fileName);
            if (fileStream == null)
            {
                return NotFound();
            }
            return File(fileStream, "application/octet-stream");
        }
    }
}
