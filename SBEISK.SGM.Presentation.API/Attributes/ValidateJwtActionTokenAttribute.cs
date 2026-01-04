using Microsoft.AspNetCore.Mvc.Filters;
using SBEISK.SGM.Domain.Permissions;
using System;

namespace SBEISK.SGM.Presentation.API.Attributes
{
    public class ValidateJwtActionTokenAttribute : Attribute, IFilterMetadata
    {
        public ActionPermissions Permission { get; internal set; }
        public ValidateJwtActionTokenAttribute(ActionPermissions permissions) => this.Permission = permissions;
    }
}
