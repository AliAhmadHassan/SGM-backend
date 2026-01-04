using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly SgmDataContext dataContext;
        public OrderRepository(SgmDataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public IList<OrderItem> OrderItems(int id, List<int?> amounts)
        {
            List<OrderItem> queryable = this.dataContext.OrderItems.Where(u => u.OrderId == id).Include(x => x.Material).ToList();
            
            for (int i = 0; i < amounts.Count; i++)
            {
                queryable[i].AmountReceived += amounts[i];
            }
            
            return queryable;
        }

        public IList<OrderStatus> Status()
        {
            var query = dataContext.OrderStatus.ToList();
            return query;
        }
    }
}
