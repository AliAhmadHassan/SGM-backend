using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class Divergence : Entity, ITimestampedModel, IUserModel
    {
        public int StatusId { get; set; }
        public int ReceivementId { get; set; }
        public ICollection<DivergenceFiles> DivergenceFiles { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get ; set; } 
        public ReceivementInvoiceOrder Receivement { get; set; }
    }
}