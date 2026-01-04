namespace SBEISK.SGM.Domain.Queries.Installation
{
    public class InstallationPaginatedQuery : PaginatedQuery
    {
        public InstallationPaginatedQuery(int page, int items) : base(page, items) { }

        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Project { get; set; }
        public bool? ThirdMaterialPermission { get; set; }
    }
}
