namespace SBEISK.SGM.Presentation.API.ViewModels.Request
{
    public class PaginatedRequest
    {
        public int Page { get; set; }
        public int Items { get; set; }
        public PaginatedRequest(int page, int items)
        {
            Page = page;
            Items = items;
        }
    }
}
