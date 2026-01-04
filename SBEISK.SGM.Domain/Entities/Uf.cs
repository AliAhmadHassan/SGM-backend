using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class Uf : Entity
    {
        public string Initials { get; set; }
        public string Name { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}