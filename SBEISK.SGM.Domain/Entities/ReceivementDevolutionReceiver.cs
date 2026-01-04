using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class ReceivementDevolutionReceiver : Entity, ITimestampedModel, IUserModel
    {
        public string Notes { get; set; }
        public DateTime ReceivementDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDraft { get; set; }
        public int UserId { get; set; }
        public int UserResponsableId { get; set; }
        public int DirectExitReceiverId { get; set; }
        public User UserResponsable { get; set; }
        public DirectExitReceiver DirectExitReceiver { get; set; }
        public ICollection<ReceivementDevolutionAttachment> Attachments { get; set; }
        public ICollection<ReceivementDevolutionEmail> Emails { get; set; }
        public ICollection<ReceivementDevolutionMaterial> DevolutionMaterialsReceiver { get; set; }
    }
}