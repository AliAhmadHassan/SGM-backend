using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class DevolutionReason : Entity
    {
        public string Description { get; set; }
        public ICollection<ReceivementDevolutionMaterial> ReceivementDevolutionMaterials { get; set; }
    }
}