using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class TransferStatus : Entity
    {
        public string Description { get; set; }
        public ICollection<Transfer> Transfers { get; set; }
    }
}