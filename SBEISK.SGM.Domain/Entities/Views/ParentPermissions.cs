namespace SBEISK.SGM.Domain.Entities.Views
{
    public class ParentPermissions
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public string ParentDescription { get; set; }
    }
}