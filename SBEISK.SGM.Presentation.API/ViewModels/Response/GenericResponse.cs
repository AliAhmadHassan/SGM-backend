using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBEISK.SGM.Presentation.API.ViewModels.Response
{
    public class GenericResponse<T> : BaseResponse
    {
        public T Data { get; set; }

        public GenericResponse() { }

        public GenericResponse(T data)
        {
            this.Data = data;
        }

        public virtual GenericResponse<T> Success()
        {
            return base.Success<GenericResponse<T>>();
        }

        public GenericResponse<T> InternalServerError()
        {
            return base.InternalServerError<GenericResponse<T>>();
        }

        public GenericResponse<T> NoModifield()
        {
            return base.NoModifield<GenericResponse<T>>();
        }
    }
}
