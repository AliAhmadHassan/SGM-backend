using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.CrossCutting.Utils.Merger;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Exceptions;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.UserProfile;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Domain.Repositories
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        private readonly SgmDataContext context;
        public UserProfileRepository(SgmDataContext context) : base(context)
        {
            this.context = context;
        }

        public string RoleByPermissionId(int id)
        {
            string role = this.context.UserProfiles.Where(p => p.Id == id).SingleOrDefault().Description;
            return role;
        }

        public PaginatedQueryResult<UserProfile> All(GenericPaginatedQuery<UserProfileQuery> query)
        {
            IQueryable<UserProfile> queryable = Applyfilter(query.Filter);

            return ApplyPagination(queryable, query);
        }

        public IQueryable<UserProfile> All(UserProfileQuery query)
        {
            return Applyfilter(query);
        }

        private IQueryable<UserProfile> Applyfilter(UserProfileQuery query)
        {
            IQueryable<UserProfile> queryable = base.Query().Where(u => u.Id > 0);

            if (query == null)
            {
                return queryable;
            }

            if (!string.IsNullOrEmpty(query.Name))
            {
                queryable = queryable.Where(x => x.Name.Contains(query.Name));
            }

            if (!string.IsNullOrEmpty(query.Description))
            {
                queryable = queryable.Where(x => x.Description.Contains(query.Description));
            }

            return queryable;
        }

        public UserProfile GetProfileWithActions(int id)
        {
            var profile  = this.context.UserProfiles
                .Include(x => x.ProfileActions)
                    .ThenInclude(profileActions => profileActions.Action)
                .FirstOrDefault(x => x.Id == id);

            if (profile == null)
            {
                throw new EntityNotFoundException($"Entity {typeof(UserProfile).Name} with given key was not found.");
            }
            return profile;
        }
        
        public void MergePermissions(ICollection<ProfileAction> original, ICollection<ProfileAction> other, Action<ProfileAction, ProfileAction> updateStrategy)
        {
            Merger<ProfileAction> merger = new Merger<ProfileAction>((x, y) => x.Id == y.Id, (x, y) => x.ActionId == y.ActionId);
            MergeResult<ProfileAction> result = merger.Merge(original.ToList(), other.ToList());

            this.DataContext.ProfileActions.RemoveRange(result.ItemsToDelete);
            this.DataContext.ProfileActions.AddRange(result.ItemsToInsert);

            foreach (var item in result.ItemsToUpdate)
            {
                updateStrategy(item.Original, item.Modified);
            }
        }
    }
}