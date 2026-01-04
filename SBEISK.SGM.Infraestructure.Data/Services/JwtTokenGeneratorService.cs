using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SBEISK.SGM.Infraestructure.Data.Services
{
    public class JwtTokenGeneratorService
    {
        private readonly IConfiguration configuration;
        private readonly IUserProfileRepository userProfileRepository;

        public JwtTokenGeneratorService(IConfiguration configuration, IUserProfileRepository userProfileRepository)
        {
            this.configuration = configuration;
            this.userProfileRepository = userProfileRepository;
        }
        
        public string GenerateAuthorizationToken(string userName, IList<UserPermission> userPermissions,User userdb, IList<UserInstallationsProfiles> userInstallationIds, int profileId)
        {
            string profile = this.userProfileRepository.RoleByPermissionId(profileId);
            
            List<Claim> claims = new List<Claim>()
            {
                new Claim("UserId", userdb.Id.ToString(), ClaimValueTypes.Integer),
                new Claim("Username", userdb.Name, ClaimTypes.Name),
                new Claim("Email", userdb.Email, ClaimValueTypes.Email),
                new Claim("RoleId", profileId.ToString(), ClaimValueTypes.Integer),
                new Claim("RoleDescription", profile, ClaimValueTypes.String),
            }.Concat(userPermissions.Select(user => new Claim("Permission", user.PermissionId.ToString(), ClaimValueTypes.Integer)))
             .Concat(userInstallationIds.Select(ins => new Claim("Installation", ins.InstallationId.ToString(), ClaimValueTypes.Integer))).ToList();

            if (userdb.UserProfileInstallations.FirstOrDefault(x => x.UserProfileId == -1) != null)
                claims.Add(new Claim("Admin", "1"));

            return GenericTokenConstructor(claims);
        }


        public string GenerateAuthenticationToken(string userName, User userdb, IList<UserInstallationsProfiles> userInstallationIds)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("UserId", userdb.Id.ToString(), ClaimValueTypes.Integer),
                new Claim("Username", userName, ClaimTypes.Name),
                new Claim("Email", userdb.Email, ClaimValueTypes.Email),
            }.Concat(userInstallationIds.Select(ins => new Claim("Installation", ins.InstallationId.ToString(), ClaimValueTypes.Integer))).ToList();

            return GenericTokenConstructor(claims);
        }

        public string GenericTokenConstructor(List<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["TokenConfigurations:Key"]);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = configuration["TokenConfigurations:Issuer"],
                Audience = configuration["TokenConfigurations:Audience"],
                Expires = DateTime.UtcNow.AddSeconds(int.Parse(configuration["TokenConfigurations:Seconds"])),
                Subject = claimsIdentity
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
