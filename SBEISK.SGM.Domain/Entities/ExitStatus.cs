using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class ExitStatus : Entity
    {
        public string Description { get; set; }
        public ICollection<DirectExit> DirectExit { get; set; }
    }
}