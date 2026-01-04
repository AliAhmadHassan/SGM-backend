namespace SBEISK.SGM.Domain.Entities.Views
{
    public class UserPermission
    {
        public int UserId { get; set; }
        public int ProfileId { get; set; }
        public int? installationId { get; set; }
        public int PermissionId { get; set; }
    }
}
