using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class InstallationType : Entity
    {
        public string Description { get; set; }
        public ICollection<Installation> Installations { get; set; }
    }
}