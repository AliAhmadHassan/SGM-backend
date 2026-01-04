using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class DirectExit : Entity
    {
        public int? ReceiverId { get; set; }
        public string VehiclePlate { get; set; }
        public string DriverName { get; set; }
        public string DriverNumber { get; set; }
        public string CompanyNameThirdy { get; set; }
        public string CNPJThirdy { get; set; }
        public int? UserDeliveryId { get; set; }
        public int? UserWithdrawId { get; set; }
        public DateTime? PrevisionDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string ReferenceLocalDelivery { get; set; }
        public string ContactDelivery { get; set; }
        public string TelephoneDelivery { get; set; }
        public int InstallationSourceId { get; set; }
        public int? InstallationDestinyId { get; set; }
        public string Notes { get; set; }
        public int? ReceiverTypeId { get; set; }
        public int? ExitStatusId { get; set; }
        public int? ExitTypeId { get; set; }
        public int? ReasonProvisionGuideId { get; set; }
        public bool IsDraft { get; set; }
        public ReasonProvisionGuide ReasonProvisionGuide { get; set; }
        public ExitStatus ExitStatus { get; set; }
        public ExitType ExitType { get; set; }
        public Receiver Receiver { get; set; }
        public User UserDelivery { get; set; }
        public User UserWithdraw { get; set; }
        public Installation IntallationSource { get; set; }
        public Installation InstallationDestiny { get; set; }
        public ReceiverType ReceiverType { get; set; }
        public ICollection<ExitMaterial> ExitMaterials { get; set; }
        public ICollection<ExitEmail> Emails { get; set; }
        public ICollection<ExitAttachment> Attachments { get; set; } 
        public ICollection<ExitPhotoBoarding> Photos { get; set; }       
    }
}