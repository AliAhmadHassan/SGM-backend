using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Infraestructure.Data.Storage.Base;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using SBEISK.SGM.Presentation.API.ViewModels.FileExample;
using SBEISK.SGM.Presentation.API.ViewModels.Response;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class FileExampleController : BaseController
    {
        private readonly IStorage storage;

        public FileExampleController(IStorage storage)
        {
            this.storage = storage;
        }
        
        [HttpPost("Upload")]
        public IActionResult Upload([FromForm]FileExampleViewModel fileModel)
        {
            FileExample fileExample = Mapper.Map<FileExample>(fileModel);

            using (Stream stream = fileModel.File.OpenReadStream())
            {
                fileExample.FileName = storage.SaveFile(fileExample, stream, fileModel.File.FileName.Split('.').Last());
                return Ok(fileExample.FileName.AsSuccessGenericResponse());
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            FileExample fileExample = new FileExample();
            storage.DeleteIfExists(fileExample.FileName);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Download(int id)
        {
            FileExample fileExample = new FileExample();
            return Ok();
        }
    }
}