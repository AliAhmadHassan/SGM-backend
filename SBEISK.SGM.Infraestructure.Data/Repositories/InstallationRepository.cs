using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.CrossCutting.Utils.Merger;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Exceptions;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Installation;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class InstallationRepository : Repository<Installation>, IInstallationRepository
    {
        private readonly SgmDataContext context;
        public InstallationRepository(SgmDataContext context) : base(context)
        {
            this.context =  context;
        }

        public PaginatedQueryResult<Installation> All(GenericPaginatedQuery<InstallationQuery> installationQuery)
        {
            IQueryable<Installation> installations = ApplyFilter(installationQuery.Filter).Include(u => u.Project).Include(u => u.InstallationType).Include(u => u.Address);

            if (installationQuery.Filter == null)
                return ApplyPagination(installations, installationQuery);

            return ApplyPagination(installations, installationQuery);
        }

        private IQueryable<Installation> ApplyFilter(InstallationQuery query)
        {
            IQueryable<Installation> queryable = this.DbSet.AsQueryable();    

            if (query == null)
            {
                return queryable;
            }

            if (query.Code.HasValue)
            {
                queryable = queryable.Where(x => x.Id.Equals(query.Code));
            }

            if(!string.IsNullOrEmpty(query.Name))
            {
                queryable = queryable.Where(x => x.Name.Contains(query.Name));
            }

            if (!string.IsNullOrEmpty(query.Description))
            {
                queryable = queryable.Where(x => x.Description.Contains(query.Description));
            }

            if(query.TypeId.HasValue)
            {
                queryable = queryable.Where(x => x.TypeId.Equals(query.TypeId));
            }

            if(query.ProjectId.HasValue)
            {
                queryable = queryable.Where(x => x.ProjectId.Equals(query.ProjectId));
            }

            if(query.AddressId.HasValue)
            {
                queryable = queryable.Where(x => x.AddressId.Equals(query.AddressId));
            }

            if(query.ThirdMaterialPermission.HasValue)
            {
                queryable = queryable.Where(x => x.ThirdMaterial.Equals(query.ThirdMaterialPermission.Value));
            }

            return queryable;
        }

        public override Installation Add(Installation entity)
        {
            InstallationType type = context.InstallationTypes.Find(entity.TypeId);
            if (type == null)
            {
                throw new EntityNotFoundException($"Entity {typeof(InstallationType).Name} with given key was not found.");
            }

            Project project = context.Projects.Find(entity.ProjectId);
            if (project == null)
            {
                throw new EntityNotFoundException($"Entity {typeof(Project).Name} with given key was not found.");
            }
            
            Address address = context.Addresses.Find(entity.AddressId);
            if (address == null)
            {
                throw new EntityNotFoundException($"Entity {typeof(Address).Name} with given key was not found.");
            }

            return base.Add(entity);
        }

        public Installation Get(int id)
        {
            var installation = this.context.Installation.FirstOrDefault(x => x.Id == id);

            if (installation == null)
            {
                throw new EntityNotFoundException($"Entity {typeof(Installation).Name} with given key was not found.");
            }
            return installation;
        }

        public void MergeResponsibles(ICollection<UserProfileInstallation> original, ICollection<UserProfileInstallation> other, Action<UserProfileInstallation, UserProfileInstallation> updateStrategy)
        {
            Merger<UserProfileInstallation> merger = new Merger<UserProfileInstallation>((x, y) => x.Id == y.Id, (x, y) => x.UserId == y.UserId);
            MergeResult<UserProfileInstallation> result = merger.Merge(original.ToList(), other.ToList());

            this.DataContext.UserProfileInstallation.RemoveRange(result.ItemsToDelete);
            this.DataContext.UserProfileInstallation.AddRange(result.ItemsToInsert);

            foreach (var item in result.ItemsToUpdate)
            {
                updateStrategy(item.Original, item.Modified);
            }
        }
        public override void Delete(int id)
        {
            Installation entity = this.Find(id);
            if (entity == null)
            {
                throw new EntityNotFoundException($"Entity {typeof(Installation).Name} with given key was not found.");
            }

            List<UserProfileInstallation> userInstallations = this.DataContext.UserProfileInstallation.Where(x => x.InstallationId == id).ToList();
            foreach (UserProfileInstallation UserProfileInstallation in userInstallations)
            {
                this.DataContext.UserProfileInstallation.Remove(UserProfileInstallation);
            }

            this.DbSet.Remove(entity);
        }
    }
}
