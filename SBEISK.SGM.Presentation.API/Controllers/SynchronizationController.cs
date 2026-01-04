using Microsoft.AspNetCore.Mvc;
using SBEISK.SGM.Domain.Services;
using SBEISK.SGM.Presentation.API.Controllers.Base;
using System.Threading.Tasks;

namespace SBEISK.SGM.Presentation.API.Controllers
{
    public class SynchronizationController : BaseController
    {
        private readonly ISynchronizationService synchronizationService;

        public SynchronizationController(ISynchronizationService synchronizationService)
        {
            this.synchronizationService = synchronizationService;
        }

        [HttpGet]
        [Route("/Synchronization/execute/{syncName}")]
        //[Authorize(Policy = "Admin")]
        public async Task<IActionResult> Execute(string syncName)
        {
            await synchronizationService.Execute(syncName);
            return Ok();
        }
    }
}
