using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface IDirectExitReceiverMaterialRepository : IRepository<DirectExitReceiverMaterial>
    {
        IEnumerable<DirectExitReceiverMaterial> AddExitReceiverMaterial(string json);

        IEnumerable<DirectExitReceiverMaterial> AddExitTemporaryCustodyMaterial(string json);
    }
}