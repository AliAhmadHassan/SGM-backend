using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories.Base;
using System.Collections.Generic;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface ICityRepository : IRepository<City>
    {
        IList<City> CitiesByUf(int uf);
    }
}
