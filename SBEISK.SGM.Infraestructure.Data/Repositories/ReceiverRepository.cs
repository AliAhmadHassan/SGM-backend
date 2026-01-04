using System.Linq;
using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Provider;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class ReceiverRepository : Repository<Receiver>, IReceiverRepository  
    {
        public ReceiverRepository(SgmDataContext dataContext) : base(dataContext)
        {
        }

        public IQueryable<Receiver> Query(ReceiverQuery query)
        {
            return ApplyFilter(query);
        }
        public PaginatedQueryResult<Receiver> All(GenericPaginatedQuery<ReceiverQuery> query)
        {        
            IQueryable<Receiver> queryable = ApplyFilter(query.Filter);
            
            return ApplyPagination(queryable, query);
        }

         private IQueryable<Receiver> ApplyFilter(ReceiverQuery query)
        {
            IQueryable<Receiver> queryable = base.Query().Include(x => x.ReceiverType);

            if (query == null)
            {
                return queryable;
            }    

            if(query.Code.HasValue)
            {
                queryable = queryable.Where(x => x.Id.Equals(query.Code));
            }     

            if (!string.IsNullOrEmpty(query.Address))
            {
                queryable = queryable.Where(x => x.Address.Contains(query.Address));
            }

            if (!string.IsNullOrEmpty(query.Description))
            {
                queryable = queryable.Where(x => x.Description.Contains(query.Description));
            }

            if (!string.IsNullOrEmpty(query.ReceiverTypeText))
            {
                queryable = queryable.Where(x => x.ReceiverType.Description.Contains(query.ReceiverTypeText));
            }

            return queryable;
        }
    }
}
