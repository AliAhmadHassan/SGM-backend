using System.Collections.Generic;

namespace SBEISK.SGM.Domain.Queries.UserProfile
{
    public class UserProfileQuery
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<int> Permission { get; set; }
    }
}