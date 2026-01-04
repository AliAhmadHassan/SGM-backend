using System.Collections.Generic;
using System.Linq;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Installation;
using SBEISK.SGM.Domain.Repositories.Queries;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories.Queries
{
    public class InstallationsReadOnlyRepository : ReadOnlyRepository<Installations>, IInstallationsReadOnlyRepository
    {

        public InstallationsReadOnlyRepository(SgmDataContext dataContext) : base(dataContext)
        {

        }
        public IList<Installations> ByInstallationId(int installationId)
        {
            return base.Query(x => x.Id == installationId).ToList();
        }
        public PaginatedQueryResult<Installations> All(GenericPaginatedQuery<InstallationQuery> query)
        {
            IQueryable<Installations> queryable = ApplyFilter(query.Filter);

            return ApplyPagination(queryable, query);
        }

        public IQueryable<Installations> All(InstallationQuery query)
        {
            return ApplyFilter(query);
        }

        private IQueryable<Installations> ApplyFilter(InstallationQuery query)
        {
            IQueryable<Installations> queryable = base.Query();

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
                queryable = queryable.Where(x => x.Name.Contains(query.Name));
            }

            if (!string.IsNullOrEmpty(query.Description))
            {
                queryable = queryable.Where(x => x.Description.Contains(query.Description));
            }

            if (query.TypeId != null)//
            {
                queryable = queryable.Where(x => x.TypeId == query.TypeId);
            }

            if (query.ProjectId != null)//
            {
                queryable = queryable.Where(x => x.ProjectId == query.ProjectId);
            }

            if (query.AddressId != null)//
            {
                queryable = queryable.Where(x => x.AddressId == query.AddressId);
            }

            if (query.ThirdMaterialPermission != null)
            {
                queryable = queryable.Where(x => x.IsThirdMaterial.Equals(query.ThirdMaterialPermission));
            }

            return queryable;
        }
    }
}