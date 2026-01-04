using System.Linq;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.BranchOffice;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class BranchOfficeRepository : Repository<BranchOffice>, IBranchOfficeRepository
    {
        public BranchOfficeRepository(SgmDataContext dataContext) : base(dataContext)
        {
        }

        public PaginatedQueryResult<BranchOffice> All(GenericPaginatedQuery<BranchOfficeQuery> query)
        {
            IQueryable<BranchOffice> queryable = ApplyFilter(query.Filter);

            return ApplyPagination(queryable, query);
        }
        public IQueryable<BranchOffice> All(BranchOfficeQuery query)
        {
            return ApplyFilter(query);
        }

        private IQueryable<BranchOffice> ApplyFilter(BranchOfficeQuery query)
        {
            IQueryable<BranchOffice> queryable = base.Query()
                .Where(provider => provider.DeletedByProcedure == false);

            if (query == null)
            {
                return queryable;
            }

            if (query.Code != null)
            {
                queryable = queryable.Where(x => x.Id.Equals(query.Code));
            }

            if (!string.IsNullOrEmpty(query.Name))
            {
                queryable = queryable.Where(x => x.Description.Contains(query.Name));
            }

            if(!string.IsNullOrEmpty(query.FantasyName))
            {
                queryable = queryable.Where(x => x.FantasyName.Contains(query.FantasyName));
            }

            if (!string.IsNullOrEmpty(query.street))
            {
                queryable = queryable.Where(x => x.Street.Contains(query.street));
            }

            if (!string.IsNullOrEmpty(query.Number))
            {
                queryable = queryable.Where(x => x.Number.Contains(query.Number));
            }

            if (!string.IsNullOrEmpty(query.Neighborhood))
            {
                queryable = queryable.Where(x => x.Neighborhood.Contains(query.Neighborhood));
            }

            if (!string.IsNullOrEmpty(query.Cep))
            {
                queryable = queryable.Where(x => x.Cep.Contains(query.Cep));
            }

            if (!string.IsNullOrEmpty(query.City))
            {
                queryable = queryable.Where(x => x.City.Contains(query.City));
            }

            if (!string.IsNullOrEmpty(query.Uf))
            {
                queryable = queryable.Where(x => x.Uf.Contains(query.Uf));
            }

            return queryable;
        }

        public bool IsUniqueName(string name, int? id = null)
        { 
    
            if(DataContext.BranchOffices.FirstOrDefault(t => t.Description == name && t.Id != id ) != null)
                return false;
            return true;
            
        }
    }
}
