using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class RequisitionOfMaterialForApplication : Entity, ITimestampedModel, IUserModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? ApprovementDate { get; set; }
        public int UserId { get; set; }
        public int? ApproverUserId { get; set; }
        public string Comments { get; set; } 
        public int StatusId { get; set; }
        public int ReceiverId { get; set; }
        public int ReceiverUserId { get; set; }
        public int InstallationId { get; set; }
        public List<RMAMaterial> Materials { get; set; }
        public List<RMAattachments> RMAattachments { get; set; }
        public User User { get; set; }
        public User ApproverUser { get; set; }
        public User ReceiverUser { get; set; }
        public Installation Installation { get; set; }
        public RMAStatus Status { get; set; }
        public Receiver Receiver { get; set; }
    }
}