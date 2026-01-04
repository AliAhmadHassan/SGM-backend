

using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Repositories.Queries;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;
using System;
using SBEISK.SGM.Domain.Queries;
using System.Linq;
using SBEISK.SGM.Domain.Queries.UserProfile;
using SBEISK.SGM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SBEISK.SGM.Infraestructure.Data.Repositories.Queries
{
    public class ProfilePermissionsReadOnlyRepository : ReadOnlyRepository<ProfilePermissions>, IProfilePermissionsReadOnlyRepository
    {
        public ProfilePermissionsReadOnlyRepository(SgmDataContext dataContext) : base(dataContext)
        {
            
        }

        public PaginatedQueryResult<ProfilePermissions> All(GenericPaginatedQuery<UserProfileQuery> query)
        {
            IQueryable<ProfilePermissions> queryable = ApplyFilter(query.Filter);
            return ApplyPagination(queryable, query);
        }

        public IQueryable<ProfilePermissions> Query(UserProfileQuery query)
        {
            return ApplyFilter(query);
        }

        private IQueryable<ProfilePermissions> ApplyFilter(UserProfileQuery query)
        {
            IQueryable<ProfilePermissions> queryable = this.DbQuery.AsQueryable().Where(u => u.ProfileId > 0);    

            if (query == null)
            {
                return queryable;
            }

            if (!string.IsNullOrEmpty(query.Name))
            {
                queryable = queryable.Where(x => x.ProfileName.Contains(query.Name));
            }

            if (!string.IsNullOrEmpty(query.Description))
            {
                queryable = queryable.Where(x => x.ProfileDescription.Contains(query.Description));
            }

            if( query.Permission != null && query.Permission.Any())
            {
                IList<int> profileIds = GetActions(query.Permission);
                queryable = queryable.Where(x => profileIds.Contains(x.ProfileId));
            }
            
            return queryable;
        }

        private List<int> GetActions(IList<int> ActionIds)
        {
            IQueryable<UserProfile> query = this.DataContext.UserProfiles.Include(x => x.ProfileActions).AsQueryable();
            foreach (var item in ActionIds)
            {
                query = query.Where(x => x.ProfileActions.Any(y => y.ActionId == item));
            }
            return query.Select(x => x.Id).Distinct().ToList();
        }
    }
}