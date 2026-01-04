using System.Net;

namespace SBEISK.SGM.Presentation.API.ViewModels.Response
{
    public enum ResponseStatus
    {
        Success = 200,
        Created = 201,
        Error = 500,
        BadRequest = 400,
        Unauthorized = 401,
        NoModified = HttpStatusCode.NotModified,
    }
}
