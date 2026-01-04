using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class Transfer : Entity, ITimestampedModel, IUserModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? PrevisionDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string VehiclePlate { get; set; }
        public string DriverName { get; set; }
        public string DriverNumber { get; set; }
        public int? InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string Complement { get; set; }
        public string Notes { get; set; }
        public int TransferStatusId { get; set; }
        public int UserId { get; set; }
        public int? UserWithdrawId { get; set; }
        public int? UserReceiverId { get; set; }
        public int STMId { get; set; }
        public bool IsDraft { get; set; }
        public TransferStatus TransferStatus { get; set; }
        public User UserReceiver { get; set; }
        public User UserWithdraw { get; set; }
        public STM STM { get; set; }
        public IList<TransferEmail> Emails { get; set; }
        public IList<TransferAttachment> Attachments { get; set; }
        public IList<TransferPhoto> Photos { get; set; }
        public ICollection<TransferMaterial> TransferMaterials { get; set; }
    }
}