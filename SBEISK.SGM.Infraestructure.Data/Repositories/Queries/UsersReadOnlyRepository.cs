using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.User;
using SBEISK.SGM.Domain.Repositories.Queries;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories.Queries
{
    public class UsersReadOnlyRepository : ReadOnlyRepository<Users>, IUsersReadOnlyRepository
    {
        public UsersReadOnlyRepository(SgmDataContext dataContext) : base(dataContext)
        {

        }

        public PaginatedQueryResult<Users> All(GenericPaginatedQuery<UserQuery> userQuery)
        {
            IQueryable<Users> users = base.Query();

            if(userQuery.Filter == null)
                return ApplyPagination(users, userQuery);
            return ApplyPagination(users, userQuery);
        }
    }
}