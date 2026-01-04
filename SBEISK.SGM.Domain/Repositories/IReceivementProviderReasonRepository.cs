using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface IReceivementProviderReasonRepository : IRepository<ReceivementProviderReason>
    {
        ReceivementProviderReason AddReceivementProviderReason(int? providerId, int? reasonId);
    }
}