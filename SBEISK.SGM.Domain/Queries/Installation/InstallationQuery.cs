namespace SBEISK.SGM.Domain.Queries.Installation
{
    public class InstallationQuery 
    {
        public int? Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? TypeId { get; set; }
        public int? ProjectId { get; set; }
        public int? AddressId { get; set; }
        public bool? ThirdMaterialPermission { get; set; }
    }
}