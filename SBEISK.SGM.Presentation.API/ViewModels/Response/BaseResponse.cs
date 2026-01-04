namespace SBEISK.SGM.Presentation.API.ViewModels.Response
{
    public class BaseResponse
    {
        public string Message { get; set; }
        public BaseResponse()
        {
            this.Message = Responses.Messages[ResponseEnum.Success];
        }

        public T Success<T>(string message = null) where T : BaseResponse
        {
            this.Message = message ?? Responses.Messages[ResponseEnum.Success];
            return (T)this;
        }
        public T InternalServerError<T>() where T : BaseResponse
        {
            this.Message = Responses.Messages[ResponseEnum.Error];
            return (T)this;
        }
        public T Error<T>(string message) where T : BaseResponse
        {
            this.Message = message;
            return (T)this;
        }

        public T NoModifield<T>(string message = null) where T : BaseResponse
        {
            this.Message = message ?? Responses.Messages[ResponseEnum.NoModified];
            return (T)this;
        }

        public T Modified<T>(string message = null) where T : BaseResponse
        {
            this.Message = message ?? Responses.Messages[ResponseEnum.Modified];
            return (T)this;
        }

        public T Removed<T>(string message = null) where T : BaseResponse
        {
            this.Message = message ?? Responses.Messages[ResponseEnum.Removed];
            return (T)this;
        }

        public T Created<T>(string message = null) where T : BaseResponse
        {
            this.Message = message ?? Responses.Messages[ResponseEnum.Created];
            return (T)this;
        }        
    }
}
