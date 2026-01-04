using System.Collections.Generic;
using SBEISK.SGM.Domain.Projections.UserProfileInstallations;

namespace SBEISK.SGM.Domain.Projections.User
{
    public class UserRequestProjection
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool? Active { get; set; }
        public List<Association> Associations { get; set; }
    }
}