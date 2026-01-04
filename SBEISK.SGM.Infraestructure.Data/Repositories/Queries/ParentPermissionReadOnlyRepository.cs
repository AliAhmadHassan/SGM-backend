using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;
using System.Linq;
using SBEISK.SGM.Domain.Repositories.Queries;
using SBEISK.SGM.Domain.Projections;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.Repositories.Queries
{
    public class ParentPermissionReadOnlyRepository : ReadOnlyRepository<ParentPermissions>, IParentPermissionReadOnlyRepository
    {
        public ParentPermissionReadOnlyRepository(SgmDataContext dataContext) : base(dataContext)
        {
            DataContext = dataContext;
        }

        public IEnumerable<ParentPermissionsProjection> GetPermissions()
        {
            var permissions = base.All();
            var grouped = permissions.GroupBy(x => x.ParentId);
            foreach (var group in grouped)
            {
                yield return new ParentPermissionsProjection()
                {
                    Id = (int)group.Key,
                    Description = group.FirstOrDefault().ParentDescription,
                    Actions = group.Select(x => new Action(){
                        Id = x.Id,
                        Description = x.Description,
                        ParentActionId = x.ParentId
                    }).ToList()
                };
            }
        }
    }
}