using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace SBEISK.SGM.Presentation.API.Extensions.Swagger
{
    public class AddHeaderParameter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }

            operation.Parameters.Add(new HeaderParameter()
            {
                Name = "Installation",
                In = "header",
                Type = "string",
                Required = false
            });
        }
    }
    class HeaderParameter : NonBodyParameter { }

}
