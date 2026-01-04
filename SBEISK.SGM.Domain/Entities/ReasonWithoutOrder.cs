using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class ReasonWithoutOrder : Entity
    {
        public string Description { get; set; }
        public ICollection<ReceivementProviderReason> ReceivementProviderReasons { get; set; }
    }
}