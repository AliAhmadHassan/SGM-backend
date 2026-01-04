using System.Collections.Generic;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public interface ISTMMaterialRepository : IRepository<STMMaterial>
    {
        IEnumerable<STMMaterial> AddSTMMaterial(string request);
    }
}