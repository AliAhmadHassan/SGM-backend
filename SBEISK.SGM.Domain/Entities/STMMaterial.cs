using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class STMMaterial : Entity
    {
        public decimal Amount { get; set; }
        public int STMId { get; set; }
        public int MaterialId { get; set; }
        public STM STM { get; set; }
        public Material Material { get; set; }
        public int Item { get; set; }
        public ICollection<TransferMaterial> TransferMaterials { get; set; }
    }
}