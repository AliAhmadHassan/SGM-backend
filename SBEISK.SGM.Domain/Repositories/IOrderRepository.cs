using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories.Base;
using System.Collections.Generic;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order>, IReadOnlyRepository<Order>
    {
       IList<OrderItem> OrderItems(int id, List<int?> array);
       IList<OrderStatus> Status();
    }
}
