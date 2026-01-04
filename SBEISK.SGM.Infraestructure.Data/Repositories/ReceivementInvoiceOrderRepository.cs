using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.ReceivementInvoiceOrderQuery;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class ReceivementInvoiceOrderRepository : Repository<ReceivementInvoiceOrder>, IReceivementInvoiceOrderRepository
    {
        private readonly SgmDataContext dataContext;
        public ReceivementInvoiceOrderRepository(SgmDataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public PaginatedQueryResult<ReceivementInvoiceOrder> All(GenericPaginatedQuery<ReceivementInvoiceOrderQuery> query)
        {
            IQueryable<ReceivementInvoiceOrder> queryable = Filter(query.Filter);
            return ApplyPagination(queryable, query);

        }

        // public IQueryable<ReceivementInvoiceOrder> Query(MaterialQuery query)
        // {
        //     return Filter(query);
        // }

        public IQueryable<ReceivementInvoiceOrder> Filter(ReceivementInvoiceOrderQuery query)
        {
            IQueryable<ReceivementInvoiceOrder> queryable = this.DbSet.AsQueryable().Include(x => x.Order).ThenInclude(x => x.Items);          

            if(query == null)
                return queryable;

            if(!string.IsNullOrEmpty(query.BranchOffice))
                queryable = queryable.Where(x => x.Installation.Project.BranchOfficeId.Equals(query.BranchOffice));

            if(query.Order.HasValue)
                queryable = queryable.Where(x => x.Order.Id.Equals(query.Order));

            if(!string.IsNullOrEmpty(query.Provider))
                queryable = queryable.Where(x => x.Order.Provider.CompanyName.Contains(query.Provider) || x.Order.Provider.Cnpj.Equals(query.Provider));

            if(!string.IsNullOrEmpty(query.Status))
                queryable = queryable.Where(x => x.Order.OrderStatus.Description.Equals(query.Status));

            if(query.DateBegin.HasValue && query.DateFinish.HasValue)
                queryable = queryable.Where(x => x.CreatedAt >= query.DateBegin && x.CreatedAt <= query.DateFinish);

            if(!string.IsNullOrEmpty(query.MaterialCode))
            {
                queryable = queryable.Where(x => x.Order.Items.Select(y => y.MaterialId).Equals(query.MaterialCode));

                // foreach (ReceivementMaterial item in queryable.Select(x => x.ReceivementsMaterials))
                // {
                //     queryable = queryable.Where(x => item.Material.Code.Equals(query.MaterialCode));
                // }
            }                
            
            if(!string.IsNullOrEmpty(query.MaterialDescription))
            {      

                 queryable = queryable.Where(x => x.Order.Items.Select(y => y.Material.Description).Equals(query.MaterialDescription));

                // foreach(ReceivementMaterial item in queryable.Select(x => x.ReceivementsMaterials))
                // {
                //     queryable = queryable.Where(x => item.Material.Description.Contains(query.MaterialDescription));
                // }
            }

            return queryable;
        }        

        public int LastIdReceiver()
        {
            int receivement = DbSet.LastOrDefault().Id;
            return receivement;
        }        
    }
}
