using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class Project : Entity, ITimestampedModel, IUserModel, ISoftDelete
    {
        public string Description { get; set; }
        public string Initials { get; set; }
        public bool Active { get; set; }
        public int UserId { get; set; }
        public int BranchOfficeId { get; set; }
        public User User { get; set; }
        public BranchOffice BranchOffice { get; set; }
        public ICollection<Installation> Installations { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}