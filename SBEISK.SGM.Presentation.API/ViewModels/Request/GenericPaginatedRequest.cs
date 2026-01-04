namespace SBEISK.SGM.Presentation.API.ViewModels.Request
{
    public class GenericPaginatedRequest<T> : PaginatedRequest
    {
        public GenericPaginatedRequest(int page, int items) : base(page, items)
        {
        }

        public T Data { get; set; }
    }
}
