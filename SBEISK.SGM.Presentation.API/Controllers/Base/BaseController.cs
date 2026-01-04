using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SBEISK.SGM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SBEISK.SGM.Presentation.API.Controllers.Base
{
    [ApiController]
    public class BaseController : Controller
    {
        protected IMapper Mapper;
        public BaseController()
        {
            
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Mapper = (IMapper)HttpContext.RequestServices.GetService(typeof(IMapper));
            base.OnActionExecuting(context);
        }

        protected int GetUserId()
        {
            return int.Parse(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId").Value.ToString());
        }

        protected IEnumerable<GenericFileClass> GetByteArray(IList<IFormFile> files)
        {
            foreach (IFormFile file in files)
            {
                string fileName = file.FileName;
                string extension = Path.GetExtension(fileName);

                using (Stream stream = file.OpenReadStream())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        byte[] arrayB = ms.ToArray();

                        GenericFileClass attach = new GenericFileClass(); //change to a generic file class
                        attach.Files = arrayB;

                        yield return attach;
                    }
                }
            }
            yield break;
        }
    }
}