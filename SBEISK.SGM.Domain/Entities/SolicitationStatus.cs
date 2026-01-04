using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class SolicitationStatus : Entity
    {
        public string Description { get; set; }
        public ICollection<STM> STMs { get; set; }
    }
}