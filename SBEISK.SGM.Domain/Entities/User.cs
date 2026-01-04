using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class User : Entity, ISoftDelete
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public string Email { get; set; }
        public DateTime? DeletedAt { get; set; }
        public ICollection<Project> Projects { get; set; }
        public virtual ICollection<UserProfileInstallation> UserProfileInstallations { get; set; }
        public ICollection<Receiver> Receivers { get; set; }
        public ICollection<STM> STMWithdraws { get; set; }
        public ICollection<STM> STMRequesters { get; set; }
        public ICollection<Transfer> TransferReceivements { get; set; }
        public ICollection<Transfer> TransferWithdraws { get; set; }
    }
}