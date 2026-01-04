using Microsoft.AspNetCore.Authorization;
using SBEISK.SGM.Domain.Permissions;

namespace SBEISK.SGM.Presentation.API.Attributes
{
    public class ActionAuthorizeAttribute : AuthorizeAttribute
    {
        public ActionAuthorizeAttribute(ActionPermissions permission)
        {
            this.Policy = permission.ToString("g");
        }
    }
}
