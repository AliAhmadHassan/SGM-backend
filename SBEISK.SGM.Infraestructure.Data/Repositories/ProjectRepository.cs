using System.Linq;
using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Exceptions;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Project;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository  
    {
        private readonly SgmDataContext context;
        public ProjectRepository(SgmDataContext context) : base(context)
        {
            this.context = context;
        }

        public override Project Add(Project entity)
        {
            BranchOffice branchOffice = DataContext.BranchOffices.Find(entity.BranchOfficeId);
            if (branchOffice == null)
            {
                throw new EntityNotFoundException($"Entity {typeof(BranchOffice).Name} with given key was not found.");
            }

            return base.Add(entity);
        }

        public PaginatedQueryResult<Project> All(GenericPaginatedQuery<ProjectQuery> query)
        {
            IQueryable<Project> queryable = ApplyFilter(query.Filter);
            
            return ApplyPagination(queryable, query);
        }

        public IQueryable<Project> Query(ProjectQuery query)
        {
            return ApplyFilter(query);
        }

        private IQueryable<Project> ApplyFilter(ProjectQuery query)
        {
            IQueryable<Project> queryable =  base.Query()
                .Include(x => x.User)
                .Include(x => x.BranchOffice);

            if(query.Code.HasValue)
            {
                queryable = queryable.Where(x => x.Id.Equals(query.Code));
            }
           
            if (!string.IsNullOrEmpty(query.Description))
            {
                queryable = queryable.Where(x => x.Description.Contains(query.Description));
            }

            if (!string.IsNullOrEmpty(query.Initials))
            {
                queryable = queryable.Where(x => x.Initials.Contains(query.Initials));
            }
 
            if (!string.IsNullOrEmpty(query.BranchOffice))
            {
                queryable = queryable.Where(x => x.BranchOffice.Description.Contains(query.BranchOffice));
            }

            if(query.Active.HasValue)
            {
                queryable = queryable.Where(x => x.Active == query.Active.Value);
            }

            return queryable;
        }

         public override void Delete(int id)
        {
            Project queryable = DataContext.Projects
                .Include(x => x.Installations)
                .FirstOrDefault(x => x.Id == id);

            if (queryable == null)
                throw new EntityNotFoundException("Projeto inexistente! Por favor verificar o código do mesmo.");

            if (queryable.Installations.Any(x => x.DeletedAt == null))
                throw new EntityCannotBeDeletedException($"Este projeto não pode ser deletado porque possui instalação ativa!");

            this.DbSet.Remove(queryable);
        }
    }
}