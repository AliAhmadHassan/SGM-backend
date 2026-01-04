using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface IExitMaterialRepository : IRepository<ExitMaterial>
    {
        IEnumerable<ExitMaterial> AddExitMaterial(string json);
    }
}