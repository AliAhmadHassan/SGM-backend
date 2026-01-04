namespace SBEISK.SGM.Domain.Entities.Views
{
    public class UserInstallationsProfiles
    {
        public int UserId { get; set; }
        public int ProfileId { get; set; }
        public int InstallationId { get; set; }
        public string InstallationDescription { get; set; }
        public int BranchOfficeId { get; set; }
        public string BranchOfficeDescription { get; set; }
        public string Location { get; set; }
    }
}
