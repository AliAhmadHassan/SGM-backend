using System;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities.Views
{
    public class ProfilePermissions : ISoftDelete
    {
        public int ProfileId { get; set; }
        public string ProfileName { get; set; }
        public string ProfileDescription { get; set; }
        public string Permissions { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}