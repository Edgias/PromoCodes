using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheRoom.PromoCodes.ApplicationCore.Interfaces;

namespace TheRoom.PromoCodes.Infrastructure.Identity
{
    public class IdentityTokenClaimService : ITokenClaimsService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityTokenClaimService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetTokenAsync(string userName)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(AuthorizationConstants.JWT_SECRET_KEY);
            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            IList<string> roles = await _userManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim> { new Claim(ClaimTypes.Name, userName) };

            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
