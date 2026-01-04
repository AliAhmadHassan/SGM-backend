using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Queries.Login;
using SBEISK.SGM.Domain.Queries.Login.Results;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Domain.Repositories.Queries;
using SBEISK.SGM.Domain.Services;
using SBEISK.SGM.CrossCutting.Configurations;
using System;

namespace SBEISK.SGM.Infraestructure.Data.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtTokenGeneratorService jwtTokenGeneratorService;
        private readonly IUserRepository userRepository;
        private readonly IUserPermissionReadOnlyRepository permissionReadOnlyRepository;
        private readonly IUserInstallationsProfilesReadOnlyRepository userInstallationsReadOnlyRepository;
        private readonly IUserService userService;
        private readonly LdapConfig ldapConfig;
        private const string MemberOfAttribute = "memberOf";
        private const string DisplayNameAttribute = "displayName";
        private const string SAMAccountNameAttribute = "sAMAccountName";
        private const string EmailAttribute = "UserPrincipalName";
        private readonly LdapConnection _connection;
        private const int MasterProfile = -1;
        private const int FakeInstallationId = -1;

        public AuthenticationService(IOptions<LdapConfig> ldapConfig, JwtTokenGeneratorService jwtTokenGeneratorService, 
        IUserRepository userRepository, IUserPermissionReadOnlyRepository permissionReadOnlyRepository, IUserInstallationsProfilesReadOnlyRepository userInstallationsReadOnlyRepository, 
        IUserService userService)
        {
            this.userRepository = userRepository;
            this.jwtTokenGeneratorService = jwtTokenGeneratorService;
            this.permissionReadOnlyRepository = permissionReadOnlyRepository;
            this.ldapConfig = ldapConfig.Value;
            this.userInstallationsReadOnlyRepository = userInstallationsReadOnlyRepository;
            this.userService = userService;
            _connection = new LdapConnection()
            {
                SecureSocketLayer = false
            };
        }
        public Task<LoginQueryResult> Authenticate(LoginQuery query)
        {
            _connection.Connect(ldapConfig.Url, ldapConfig.Port);
            _connection.Bind(ldapConfig.BindDn, ldapConfig.BindCredentials);
            LdapSearchConstraints cons = _connection.SearchConstraints;
            cons.ReferralFollowing = true;
            _connection.Constraints = cons;
            string searchFilter = string.Format(ldapConfig.SearchFilter, query.Username.Split('@').First());

            LdapSearchResults result = _connection.Search(ldapConfig.SearchBase, LdapConnection.SCOPE_SUB, searchFilter, 
                new[] { MemberOfAttribute, DisplayNameAttribute, SAMAccountNameAttribute },
                false
            );

            if (result.hasMore())
            {
                var user = result.next();
                
                try
                {
                    _connection.Bind(user.DN, query.Password);
                }
                catch (LdapException)
                {
                    _connection.Disconnect();
                    return UserNotFound();
                }

                if (_connection.Bound)
                { 
                    User userdb = userRepository.UserByEmail(query.Username);
                    
                    if (userdb == null)
                    {
                        return Task.FromResult(new LoginQueryResult()
                        {
                            Token = string.Empty,
                            Succeeded = false,
                            Username = string.Empty,
                            Message = "Usuário de rede não localizado no sistema, favor contactar o administrador"
                        });
                    }
      
                        

                    IList<UserInstallationsProfiles> userInstallations = userInstallationsReadOnlyRepository.ByUserId(userdb.Id);
                    
                    if(userInstallations.ToArray().Length == 0)
                        userInstallations.Add( new UserInstallationsProfiles{ InstallationId = FakeInstallationId } );
                    
                    string userName = user.getAttribute(SAMAccountNameAttribute).StringValue;

                    _connection.Disconnect();

                    return Task.FromResult(new LoginQueryResult
                    {
                        DisplayName = userdb.Name,
                        Username = userdb.Name,
                        Token = jwtTokenGeneratorService.GenerateAuthenticationToken(userName, userdb, userInstallations),
                        Succeeded = true
                    });
                }
            }

            _connection.Disconnect();

            return UserNotFound();
        }

        public Task<string> Authorize(int userId, int installationId)
        {
            User userdb = userRepository.GetWithInstallations(userId);
            string userName = userdb.Email.Split('@').First();
            UserProfileInstallation installationProfile = userdb.UserProfileInstallations.First();
            int profileId;
            IList<UserInstallationsProfiles> userInstallations = new List<UserInstallationsProfiles>();

            if(!(installationProfile.InstallationId == null && installationProfile.UserProfileId == MasterProfile))
            {
                var requestedInstallation = userdb.UserProfileInstallations.FirstOrDefault(x => x.InstallationId == installationId);
                    if (requestedInstallation == null)
                        return Task.FromResult(string.Empty);
                
                profileId = userdb.UserProfileInstallations.FirstOrDefault(x => x.InstallationId == installationId && x.UserId == userId).UserProfileId;
                
                userInstallations = userInstallationsReadOnlyRepository.ByUserId(userdb.Id);
            }  
            
            else
            {
                userInstallations = userService.GetMasterInstallations(userdb).ToList();
                profileId = MasterProfile;
            }
            
            IList<UserPermission> userPermissions = permissionReadOnlyRepository.ByUserIdByInstallation(userdb.Id, profileId == MasterProfile ? null : (int?)installationId);
            string authorizationToken = jwtTokenGeneratorService.GenerateAuthorizationToken(userName, userPermissions, userdb, userInstallations, profileId);
            return Task.FromResult(authorizationToken);
        }

            private Task<LoginQueryResult> UserNotFound()
        {
            return Task.FromResult(new LoginQueryResult()
            {
                Token = string.Empty,
                Succeeded = false,
                Username = string.Empty
            });
        }

        public Task<List<UserQueryResult>> GetUsers()
        {
            _connection.Connect(ldapConfig.Url, ldapConfig.Port);
            _connection.Bind(ldapConfig.BindDn, ldapConfig.BindCredentials);
            LdapSearchConstraints cons = _connection.SearchConstraints;
            cons.ReferralFollowing = true;
            _connection.Constraints = cons;

            string searchFilter = ldapConfig.UsersFilter;

            LdapSearchResults results = _connection.Search(ldapConfig.SearchBase, LdapConnection.SCOPE_SUB, searchFilter, 
                new[] { DisplayNameAttribute, EmailAttribute },
                false
            );

            List<UserQueryResult> users = new List<UserQueryResult>();
            while (results.hasMore())
            {
                var result = results.next();
                UserQueryResult user = new UserQueryResult()
                {
                    Name = result.getAttribute(DisplayNameAttribute).StringValue,
                    Email = System.Text.RegularExpressions.Regex.Split(result.getAttribute(EmailAttribute).StringValue, ".br").First()
                }; 
                users.Add(user);
            }

            _connection.Disconnect();

            return Task.FromResult(users.OrderBy(n => n.Name).ToList());
        }
    }
}
