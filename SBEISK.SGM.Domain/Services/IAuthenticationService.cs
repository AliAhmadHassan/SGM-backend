using SBEISK.SGM.Domain.Queries.Login;
using SBEISK.SGM.Domain.Queries.Login.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBEISK.SGM.Domain.Services
{
    public interface IAuthenticationService
    {
        Task<LoginQueryResult> Authenticate(LoginQuery query);
        Task<string> Authorize(int userId, int installationId);
        Task<List<UserQueryResult>> GetUsers();
    }
}
