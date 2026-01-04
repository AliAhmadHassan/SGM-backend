using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class UserProfileInstallation : Entity
    {
        public int UserId { get; set; }
        public int UserProfileId { get; set; }
        public int? InstallationId { get;  set; }
        public User User { get; set; }
        public Installation Installation { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
