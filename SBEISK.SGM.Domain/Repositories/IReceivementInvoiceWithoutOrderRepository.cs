using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Projections.Material;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface IReceivementInvoiceWithoutOrderRepository : IRepository<ReceivementInvoiceOrder>
    {
        // List<WithoutOrderProjection> WithoutOrders(string materialCodes);
        decimal CalculateTotalPrice(decimal amount, decimal price);
    }
}