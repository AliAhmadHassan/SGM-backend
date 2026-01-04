using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class BranchOffice : Entity
    {
        public string Description { get; set; }
        public string Cnpj { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string Cep { get; set; }
        public string Uf { get; set; }
        public string FantasyName { get; set; }
        public bool DeletedByProcedure { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
