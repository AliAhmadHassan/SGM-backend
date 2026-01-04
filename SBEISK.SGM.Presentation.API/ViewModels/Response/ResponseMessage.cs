using SBEISK.SGM.Presentation.API.ViewModels.Response;
using System.Collections.Generic;

namespace SBEISK.SGM.Presentation.API.ViewModels.Response
{
    public static class ResponseMessages
    {
        public static IDictionary<ResponseEnum, ResponseStatus> Messages = new Dictionary<ResponseEnum, ResponseStatus>()
        {
            { ResponseEnum.Error, ResponseStatus.Error },
            { ResponseEnum.Success, ResponseStatus.Success },
            { ResponseEnum.NoModified, ResponseStatus.NoModified },
            { ResponseEnum.Created, ResponseStatus.Success },
            { ResponseEnum.Removed, ResponseStatus.Success },
            { ResponseEnum.Modified, ResponseStatus.Success }
        };
    }
}