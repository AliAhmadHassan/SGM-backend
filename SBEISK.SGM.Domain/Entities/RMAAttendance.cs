using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class RMAAttendance : Entity, ITimestampedModel, IUserModel
    {
        public bool IsDraft { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeliverAt { get; set; }
        public string Comments { get; set; } 
        public int UserId { get; set; }
        public int? DeliverUserId { get; set; }
        public int? ReceiverUserId { get; set; }
        public User User { get; set; }
        public User DeliverUser { get; set; }
        public User ReceiverUser { get; set; }
        public List<RMAAttendanceMaterial> Materials { get; set; }
        public List<RMAAttendanceAttachments> Attachments { get; set; }
        public List<RMAAttendanceEmails> Emails { get; set; }
    }
}
