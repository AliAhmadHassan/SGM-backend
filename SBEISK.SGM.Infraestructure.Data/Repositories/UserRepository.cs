using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.CrossCutting.Utils.Merger;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Exceptions;
using SBEISK.SGM.Domain.Projections.User;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.User;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Domain.Status;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly SgmDataContext context;
        public UserRepository(SgmDataContext context) : base(context)
        {
            this.context = context;
        }
        public User UserByEmail(string email)
        {
            User user = this.context.Users.Where(u => u.Email == email).SingleOrDefault();

            if (user != default(User))
                return user;
            return null;
        }

        public int UserId()
        {
            return this.context.Users.Last().Id;
        }

        public UserRequestProjection NewUser(UserRequestProjection userRequest)
        {
            User userQuery = this.context.Users.Where(u => u.Email == userRequest.Email).FirstOrDefault();

            foreach (var item in userRequest.Associations)
            {
                UserProfile profile = DataContext.UserProfiles.Find(item.UserProfileId);
                if (profile == null || profile.Id == -1)
                {
                    throw new EntityNotFoundException($"Entity {typeof(UserProfile).Name} with given key was not found.");
                }
            }

            if (userQuery is User)
                return null;

            if (ValidateFields(userRequest))
            {
                Add(new User
                {
                    Name = userRequest.Name,
                    Active = userRequest.Active.Value,
                    Email = userRequest.Email
                });

                SaveChanges();
            }
            return userRequest;
        }

        private bool ValidateFields(UserRequestProjection request)
        {
            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrWhiteSpace(request.Name))
                return false;
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrWhiteSpace(request.Email))
                return false;
            if (request.Associations == null || !request.Active.HasValue)
                return false;
            return true;
        }

        public UserRequestProjection EditUser(UserRequestProjection userRequest, int id)
        {
            User user = this.DbSet.FirstOrDefault(u => u.Id == id);

            user.Name = userRequest.Name;
            user.Active = userRequest.Active.Value;
            user.Email = userRequest.Name;

            SaveChanges();
            return userRequest;
        }

        public PaginatedQueryResult<User> All(GenericPaginatedQuery<UserQuery> userQuery)
        {
            IQueryable<User> users = Query(userQuery.Filter).Include(u => u.UserProfileInstallations).ThenInclude(u => u.UserProfile)
                                                            .Include(u => u.UserProfileInstallations).ThenInclude(u => u.Installation);

            if (userQuery.Filter == null)
                return ApplyPaginationByName(users, userQuery);

            return ApplyPaginationByName(users, userQuery);
        }

        private PaginatedQueryResult<User> ApplyPaginationByName(IQueryable<User> queryable, PaginatedQuery query)
        {
            IQueryable<User> items = queryable.OrderBy(x => x.Name).Skip((Math.Max(query.Page, 1) - 1) * query.Items).Take(query.Items);
            return new PaginatedQueryResult<User>(items.ToList(), queryable.Count());
        }

        public User GetWithInstallations(int id)
        {
            return context.Users.Include(upi => upi.UserProfileInstallations).FirstOrDefault(u => u.Id == id);
        }

        public void MergeUsers(ICollection<UserProfileInstallation> original, ICollection<UserProfileInstallation> other, Action<UserProfileInstallation, UserProfileInstallation> updateStrategy)
        {
            foreach (var item in other)
            {
                var installation = DataContext.Installation.Find(item.InstallationId);
                if (installation == null)
                {
                    throw new EntityNotFoundException($"Entity {typeof(UserProfile).Name} with given key was not found.");
                }
            }

            foreach (var item in other)
            {
                UserProfile profile = DataContext.UserProfiles.Find(item.UserProfileId);
                if (profile == null || profile.Id == -1)
                {
                    throw new EntityNotFoundException($"Entity {typeof(UserProfile).Name} with given key was not found.");
                }
            }
            Merger<UserProfileInstallation> merger = new Merger<UserProfileInstallation>((x, y) => x.Id == y.Id, (x, y) => x.InstallationId == y.InstallationId);
            MergeResult<UserProfileInstallation> result = merger.Merge(original.ToList(), other.ToList());

            this.DataContext.UserProfileInstallation.RemoveRange(result.ItemsToDelete);
            this.DataContext.UserProfileInstallation.AddRange(result.ItemsToInsert);

            foreach (var item in result.ItemsToUpdate)
            {
                updateStrategy(item.Original, item.Modified);
            }
        }

        public IQueryable<User> Query(UserQuery query)
        {
            return ApplyFilter(query);
        }

        private IQueryable<User> ApplyFilter(UserQuery query)
        {
            IQueryable<User> queryable = this.DbSet.AsQueryable();

            if (query == null)
            {
                return queryable;
            }

            if (!string.IsNullOrEmpty(query.UserName))
            {
                queryable = queryable.Where(x => x.Name.Contains(query.UserName));
            }

            if (!string.IsNullOrEmpty(query.InstallationName))
            {
                queryable = queryable.Where(x => string.Join(", ", x.UserProfileInstallations.Select(ins => ins.Installation.Name)).Contains(query.InstallationName));
            }

            if (query.Active.HasValue)
            {
                queryable = queryable.Where(x => x.Active.Equals(query.Active.Value));
            }

            return queryable;
        }

        public UserStatus Authorization(string email)
        {
            if (context.Users.FirstOrDefault(t => t.Email == email).DeletedAt != null)
                return UserStatus.Deleted;

            if (context.Users.FirstOrDefault(t => t.Email == email).Active)
                return UserStatus.Activated;

            return UserStatus.Deactivated;
        }

        public bool ValidateUserInstallations(string email)
        {
            var array = context.Users.Include(x => x.UserProfileInstallations).FirstOrDefault(x => x.Email == email).UserProfileInstallations;

            if (array == null || array.ToArray().Length == 0)
                return false;
            return true;
        }
    }
}