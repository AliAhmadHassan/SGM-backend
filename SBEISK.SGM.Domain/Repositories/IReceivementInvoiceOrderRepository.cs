using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.ReceivementInvoiceOrderQuery;
using SBEISK.SGM.Domain.Repositories.Base;

namespace  SBEISK.SGM.Domain.Repositories
{
    public interface IReceivementInvoiceOrderRepository : IRepository<ReceivementInvoiceOrder>
    {
        PaginatedQueryResult<ReceivementInvoiceOrder> All(GenericPaginatedQuery<ReceivementInvoiceOrderQuery> query);
        int LastIdReceiver();
    }
}