using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface IDirectExitReceiverRepository : IRepository<DirectExitReceiver>
    {
        int LastIdDirectExit();
    }
}