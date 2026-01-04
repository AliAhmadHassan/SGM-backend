using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.ReceivementInvoiceOrderQuery;
using SBEISK.SGM.Domain.Repositories.Base;
using System.Collections.Generic;

namespace SBEISK.SGM.Domain.Repositories.Queries
{
    public interface IReceivementInvoiceOrderReadOnlyRepository : IReadOnlyRepository<ReceivementInvoiceOrders>
    {
        PaginatedQueryResult<ReceivementInvoiceOrders> All(GenericPaginatedQuery<ReceivementInvoiceOrderQuery> receivementInvoiceOrderQuery);
        ReceivementInvoiceOrders WithItems(int index);
    }
}
