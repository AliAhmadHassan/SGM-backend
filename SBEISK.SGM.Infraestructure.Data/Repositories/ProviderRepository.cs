using System.Collections.Generic;
using System.Linq;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Provider;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(SgmDataContext dataContext) : base(dataContext)
        {
        }

        public IList<Provider> WithItems()
        {
            return DataContext.Providers.ToList();
        }

        public PaginatedQueryResult<Provider> All(GenericPaginatedQuery<ProviderQuery> query)
        {
            IQueryable<Provider> queryable = ApplyFilter(query.Filter);

            return ApplyPagination(queryable, query);
        }
        public IQueryable<Provider> All(ProviderQuery query)
        {
            return ApplyFilter(query);
        }

        private IQueryable<Provider> ApplyFilter(ProviderQuery query)
        {
            IQueryable<Provider> queryable = this.DbSet
                .AsQueryable()
                .Where(provider => provider.DeletedByProcedure == false);

            if (query == null)
            {
                return queryable;
            }

            if (!string.IsNullOrEmpty(query.Cnpj))
            {
                queryable = queryable.Where(x => x.Cnpj.Contains(query.Cnpj));
            }

            if (!string.IsNullOrEmpty(query.CompanyName))
            {
                queryable = queryable.Where(x => x.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrEmpty(query.FantasyName))
            {
                queryable = queryable.Where(x => x.FantasyName.Contains(query.FantasyName));
            }

            if (!string.IsNullOrEmpty(query.Telephone))
            {
                queryable = queryable.Where(x => x.Telephone.Contains(query.Telephone));
            }

            if (!string.IsNullOrEmpty(query.Street))
            {
                queryable = queryable.Where(x => x.Street.Contains(query.Street));
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
    }
}
