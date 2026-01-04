using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class Material : Entity, ISoftDelete
    {
        public string Description{ get; set; }
        public string Code { get; set; }
        public int MaterialSubFamilyId { get; set; }
        public bool DeletedByProcedure { get; set; }
        public MaterialSubFamily SubFamily { get ; set;}
        public int SbeiCode { get; set; }
        public string Unity { get; set; }
        public decimal? UnitCost { get; set; }
        public DateTime? DeletedAt { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<TransferMaterial> TransferMaterials { get; set; }
        public ICollection<STMMaterial> STMMaterials { get; set; }
        public ICollection<ReceivementMaterial> ReceivementMaterials { get; set; }
    }
}