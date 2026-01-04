using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class OrderItem : Entity, ISoftDelete
    {
        public decimal Quantity { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int MaterialId { get; set; }
        public Material Material { get; set; }
        public DateTime? DeletedAt { get; set; }
        public short? SequenceNumber { get; set; }
        public decimal? AmountReceived { get; set; }
        public decimal? UnitaryCost  {get; set; }
        public bool? ProcedureDeleted { get; set;} 
        public ICollection<ReceivementMaterial> ReceivementMaterials { get; set; }
    }
}
