using ApiDomain.Entity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiApplication.Helpers.JWT
{
    public static class TokenGenerator
    {
        public static string Generate(ApplicationUser user, List<string> claims, JwtConfig config,DateTime expireDate)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id),new Claim("companyId", user.CompanyId.ToString()) }),
                Expires = expireDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            foreach (var claim in claims)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(claim, claim));
            }
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}