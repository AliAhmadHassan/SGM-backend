namespace SBEISK.SGM.Domain.Queries.BranchOffice
{
    public class BranchOfficePaginatedQuery : PaginatedQuery
    {
        public BranchOfficePaginatedQuery(int page, int items) : base(page, items) { }

        public string Name { get; set; }
    }
}
