using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class ReceivementInvoiceOrder : Entity, ITimestampedModel, IUserModel
    {
        public bool IsDraft { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Invoice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? InvoiceCreatedAt { get; set; }
        public DateTime? ReceivementDate { get; set; }
        public bool ThirdPartyMaterial { get; set; } 
        public string Complement { get; set; }
        public string Note { get; set; }
        public string VehiclePlate { get; set; }    
        public string DriverName { get; set; }
        public string DriverNumber { get; set; }
        public int? OrderId { get; set; }
        public int UserId  { get; set; }
        public int? ReceiverUser { get; set; }
        public int? InvoiceTypeId { get; set; }
        public int? InstallationId { get; set; }
        public Order Order { get; set; }
        public Installation Installation { get; set; }
        public IList<ReceivementMaterial> ReceivementsMaterials { get; set; }
        public IList<ReceivementAttachment> ReceivementAttachments { get; set; }
        public IList<ReceivementEmail> ReceivementEmails { get; set; }
        public IList<ReceivementPhoto> ReceivementPhotos { get; set; }
        public ReceivementProviderReason ReceivementProviderReasons { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public User UserReceivementInvoiceOrder { get; set; }
    }
}