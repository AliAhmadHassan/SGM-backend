using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class Receiver : Entity, ITimestampedModel, IUserModel, ISoftDelete
    {
        public string Description { get; set; }
        public int UserId { get; set; }
        public int ReceiverTypeId { get; set; }
        public string Address { get; set; }
        public ReceiverType ReceiverType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public User User { get; set; }
        public List<RequisitionOfMaterialForApplication> RMA { get; set; }
    }
}