
using System.Collections.Generic;
using System.Linq;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.ReceivementInvoiceOrderQuery;
using SBEISK.SGM.Domain.Repositories.Queries;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories.Queries
{
    public class ReceivementInvoiceOrderReadOnlyRepository : ReadOnlyRepository<ReceivementInvoiceOrders>, IReceivementInvoiceOrderReadOnlyRepository
    {
        public ReceivementInvoiceOrderReadOnlyRepository(SgmDataContext dataContext) : base(dataContext)
        {

        }

        public IQueryable<ReceivementInvoiceOrders> Filter(ReceivementInvoiceOrderQuery query)
        {
            IQueryable<ReceivementInvoiceOrders> queryable = base.Query();

            if(query == null)
                return queryable;

            if(!string.IsNullOrEmpty(query.BranchOffice))
                queryable = queryable.Where(x => x.BranchOfficeDescription.Contains(query.BranchOffice));

            if(query.Order.HasValue)
                queryable = queryable.Where(x => x.OrderCode.Equals(query.Order));

            if(!string.IsNullOrEmpty(query.Provider))
                queryable = queryable.Where(x => x.ProviderName.Contains(query.Provider) || x.CNPJ.Equals(query.Provider));

            if(!string.IsNullOrEmpty(query.Status))
                queryable = queryable.Where(x => x.OrderStatus == query.Status);

            if(query.DateBegin.HasValue && query.DateFinish.HasValue)
                queryable = queryable.Where(x => x.OrderEmission >= query.DateBegin && x.OrderEmission <= query.DateFinish);

            if(!string.IsNullOrEmpty(query.MaterialCode))
            {
                int materialId = DataContext.Materials.Where(x => x.Code.Contains(query.MaterialCode)).FirstOrDefault().Id;
                IList<OrderItem> orderItems = DataContext.OrderItems.Where(x => x.MaterialId.Equals(materialId)).ToList();
                queryable = queryable.Where(item => orderItems.Any(orderItem => orderItem.OrderId == item.OrderCode));  
            }                
            
            if(!string.IsNullOrEmpty(query.MaterialDescription))
            {        
                IQueryable<Material> materials = DataContext.Materials.Where(x => x.Description.Contains(query.MaterialDescription));
                IList<OrderItem> orderItems = DataContext.OrderItems.Where(item => materials.Any(material => material.Id.Equals(item.MaterialId))).ToList();
                queryable = queryable.Where(item => orderItems.Any(orderItem => orderItem.OrderId == item.OrderCode));    
            }

            return queryable;
        }

        public PaginatedQueryResult<ReceivementInvoiceOrders> All(GenericPaginatedQuery<ReceivementInvoiceOrderQuery> receivementInvoiceOrderQuery)
        {
            IQueryable<ReceivementInvoiceOrders> receivements = Filter(receivementInvoiceOrderQuery.Filter);

            return ApplyPagination(receivements, receivementInvoiceOrderQuery);
        }

        public ReceivementInvoiceOrders WithItems(int id)
        {
            ReceivementInvoiceOrders receivements = base.Query().FirstOrDefault(x => x.OrderCode == id);
            return receivements;
        }
    }
}