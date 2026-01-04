using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class ExitType : Entity
    {
        public int Description { get; set; }
        public ICollection<DirectExit> DirectExits { get; set; }
    }
}