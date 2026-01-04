using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Material;
using System.Linq;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        public MaterialRepository(SgmDataContext dataContext): base(dataContext)
        {
        }
        public PaginatedQueryResult<Material> All(GenericPaginatedQuery<MaterialQuery> query)
        {
            IQueryable<Material> queryable = ApplyFilter(query.Filter);
            return ApplyPagination(queryable, query);
        }

        public IQueryable<Material> Query(MaterialQuery query)
        {
            return ApplyFilter(query);
        }

        private IQueryable<Material> ApplyFilter(MaterialQuery query)
        {
            IQueryable<Material> queryable = this.DbSet.AsQueryable()
                .Where(provider => provider.DeletedByProcedure == false);

            if (query == null)
            {
                return queryable;
            }

            if (!string.IsNullOrEmpty(query.Code))
            {
                queryable = queryable.Where(x => x.Code.Contains(query.Code));
            }

            if (!string.IsNullOrEmpty(query.Description))
            {
                queryable = queryable.Where(x => x.Description.Contains(query.Description));
            }

            if (!string.IsNullOrEmpty(query.Unit))
            {
                queryable = queryable.Where(x => x.Unity.Contains(query.Unit));
            }

            return queryable;
        }

        public Material MaterialByCode(string materialCode)
        {
            return Query().FirstOrDefault(x => x.Code.Equals(materialCode));             
        }
    }
}