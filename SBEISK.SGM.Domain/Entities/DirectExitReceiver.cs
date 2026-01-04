using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class DirectExitReceiver : Entity
    {
        public int UserDeliveryId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int UserWithdrawId { get; set; }
        public int InstallationId { get; set; }
        public string Notes { get; set; }
        public int ReceiverId { get; set; }
        public Receiver Receiver { get; set; }
        public User UserDelivery { get; set; }
        public User UserWithdraw { get; set; }
        public Installation Installation { get; set; }
        public ICollection<DirectExitReceiverEmail> Emails { get; set; }
        public ICollection<DirectExitReceiverAttachment> Attachments { get; set; }
        public ICollection<DirectExitReceiverMaterial> DirectExitReceiverMaterials { get; set; }
        public ICollection<ReceivementDevolutionReceiver> ReceivementDevolutionReceivers { get; set; }
    }
}