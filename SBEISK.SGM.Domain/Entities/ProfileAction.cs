using System;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class ProfileAction : Entity, ITimestampedModel, IUserModel
    {
        public int ProfileId { get; set; }
        public int ActionId { get; set; }
        public Action Action { get; set; }
        public UserProfile Profile { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
    }
}