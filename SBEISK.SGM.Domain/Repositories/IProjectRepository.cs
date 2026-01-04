using System.Linq;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Project;
using SBEISK.SGM.Domain.Repositories.Base;

namespace  SBEISK.SGM.Domain.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        PaginatedQueryResult<Project> All(GenericPaginatedQuery<ProjectQuery> query);
        IQueryable<Project> Query(ProjectQuery query);
    }
}