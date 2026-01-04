namespace SBEISK.SGM.Domain.Queries
{
    public class GenericPaginatedQuery<TEntity> : PaginatedQuery
    {
        public TEntity Filter { get; set; }
        public GenericPaginatedQuery(int page, int items, TEntity filter) : base(page, items)
        {
            Filter = filter;
        }
    }
}
