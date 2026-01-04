using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace SBEISK.SGM.Presentation.API.Attributes
{
    public class ValidateJwtTokenAttribute : Attribute, IFilterMetadata
    {
    }
}
