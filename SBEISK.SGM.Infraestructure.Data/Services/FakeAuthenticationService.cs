using System.Collections.Generic;
using System.Threading.Tasks;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Queries.Login;
using SBEISK.SGM.Domain.Queries.Login.Results;
using SBEISK.SGM.Domain.Services;

namespace SBEISK.SGM.Infraestructure.Data.Services
{
    public class FakeAuthenticationService : IAuthenticationService
    {
        private readonly JwtTokenGeneratorService jwtGenerationToken;

        public FakeAuthenticationService(JwtTokenGeneratorService jwtGenerationToken)
        {
            this.jwtGenerationToken = jwtGenerationToken;
        }
        public Task<LoginQueryResult> Authenticate(LoginQuery query)
        {
            //IList<UserPermission> UserPermissions = new List<UserPermission>();
            User user = new User();
            List<UserInstallationsProfiles> userInstallationIds = new List<UserInstallationsProfiles>();
            
            return Task.FromResult(
                new LoginQueryResult()
                {
                    Token = jwtGenerationToken.GenerateAuthenticationToken(query.Username, user, userInstallationIds),
                    DisplayName = query.Username,
                    Username = query.Username, 
                    Succeeded = true
                }
            );
        }

        public Task<string> Authorize(int userId, int installationId)
        {
            return Task.FromResult(string.Empty);
        }

            public Task<List<UserQueryResult>> GetUsers()
        {
            throw new System.NotImplementedException();
        }
    }
}
