using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class UserProfile : Entity, ITimestampedModel, IUserModel, ISoftDelete
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
        public ICollection<ProfileAction> ProfileActions { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}