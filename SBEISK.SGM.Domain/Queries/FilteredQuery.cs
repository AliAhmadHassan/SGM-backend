namespace SBEISK.SGM.Domain.Queries
{
    public class FilteredQuery<TEntity>
    {
        public TEntity Filter { get; set; }

        public FilteredQuery(TEntity filter)
        {
            Filter = filter;
        }
    }
}
