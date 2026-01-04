using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface ITransferRepository : IRepository<Transfer>
    {
        PaginatedQueryResult<Transfer> All(GenericPaginatedQuery<STMQuery> userQuery);
        int LastIdTransfer();
        Transfer TransferById(int id);
        void MergeTransfer(ICollection<Transfer> original, ICollection<Transfer> other, Action<Transfer, Transfer> updateStrategy);
    }
}