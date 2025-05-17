using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnionForceSpin.Application.Interfaces.Tokens;
using OnionForceSpin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnionForceSpin.Infrastructure.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly TokenSettings tokenSettings;
        private readonly UserManager<User> userManager;

        public TokenService(IOptions<TokenSettings> options, UserManager<User> userManager)
        {
            tokenSettings = options.Value;
            this.userManager = userManager;
        }
        public string CreateRefreshToken()
        {
            return null;
        }

        public async Task<JwtSecurityToken> CreateToken(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //Jwt Id'yi temsil eder
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email!),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Secret));

            var token = new JwtSecurityToken(
                issuer: tokenSettings.Issuer,
                audience: tokenSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(tokenSettings.TokenValidityInMinutes),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            foreach (var claim in claims)
            {
                await userManager.AddClaimAsync(user, claim);
            }
            return token;
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
