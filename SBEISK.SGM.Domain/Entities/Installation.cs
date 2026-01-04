using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class Installation : Entity, ITimestampedModel, IUserModel, ISoftDelete
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool ThirdMaterial { get; set; }
        public int AddressId { get; set; }
        public int TypeId { get; set; }
        public int ProjectId { get; set; }
        public Address Address { get; set; }
        public InstallationType InstallationType { get; set; }
        public Project Project { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public ICollection<ReceivementInvoiceOrder> ReceivementInvoiceOrders { get; set; }
        public ICollection<RequisitionOfMaterialForApplication> RMAs { get; set; }
        public ICollection<STM> STMsSources { get; set; }
        public ICollection<STM> STMsDestiny { get; set; }
    }
}
