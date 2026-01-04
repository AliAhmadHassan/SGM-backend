using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Installation;
using SBEISK.SGM.Domain.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace SBEISK.SGM.Domain.Repositories.Queries
{
    public interface IInstallationsReadOnlyRepository : IReadOnlyRepository<Installations>
    {
        IList<Installations> ByInstallationId(int installationId);
        
        PaginatedQueryResult<Installations> All(GenericPaginatedQuery<InstallationQuery> query);
        IQueryable<Installations> All(InstallationQuery query);
    }
}
