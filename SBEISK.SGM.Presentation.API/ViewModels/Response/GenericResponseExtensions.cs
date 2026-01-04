namespace SBEISK.SGM.Presentation.API.ViewModels.Response
{
    public static class GenericResponseExtensions
    {
        public static GenericResponse<object> AsSuccessGenericResponse(this object data)
        {
            return new GenericResponse<object>(data);
        }

        public static GenericResponse<object> AsNoModifieldGenericResponse(this object data)
        {
            return new GenericResponse<object>().NoModifield();
        }
    }
}
