using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class City : Entity
    {
        public string Name { get; set; }
        public int UfId { get; set; }
        public virtual Uf Uf { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}