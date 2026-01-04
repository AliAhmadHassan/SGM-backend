using System;
namespace SBEISK.SGM.Domain.Queries.User
{
    public class UserQuery
    {
        public string UserName { get; set; }
        public string InstallationName { get; set; }    
        public bool? Active { get; set; }    
    }
}