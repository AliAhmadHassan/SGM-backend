using System.Collections.Generic;

namespace SBEISK.SGM.Domain.Projections.User
{
    public class InstalationsBranchOffice
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public IEnumerable<Installation> Instalations { get; set; }
    }
}
