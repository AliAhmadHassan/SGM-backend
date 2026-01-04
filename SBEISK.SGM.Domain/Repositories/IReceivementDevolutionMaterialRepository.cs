using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface IReceivementDevolutionMaterialRepository : IRepository<ReceivementDevolutionMaterial>
    {        
        IEnumerable<ReceivementDevolutionMaterial> AddReceivementDevolutionMaterial(string json);
    }
}