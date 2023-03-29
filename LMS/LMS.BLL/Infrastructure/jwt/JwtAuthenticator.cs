using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
 
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
 
using LMS.BLL.Infrastructures.jwt;
 
using LMS.DAL.Entities.identityEntities;
using LMS.BLL.DTOs.Response;
using LMS.BLL.Interfaces;

namespace LMS.BLL.Infrastructure.jwt
{
    public class JwtAuthenticator : IJWTAuthenticator
    {
        private readonly JwtConfig _jwtConfig;

        public JwtAuthenticator(JwtConfig jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }


        public async Task<JwtToken> GenerateJwtToken(AppUser user, string expires = null,
            List<Claim> additionalClaims = null)
        {
            JwtSecurityTokenHandler jwtTokenHandler = new();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            IdentityOptions _options = new();

            var claims = new List<Claim>
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(_options.ClaimsIdentity.UserIdClaimType, user.Id),
                new Claim(_options.ClaimsIdentity.UserNameClaimType, user.UserName),
            };

            if (additionalClaims != null)
            {
                claims.AddRange(additionalClaims);
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = string.IsNullOrWhiteSpace(expires)
                    ? DateTime.Now.AddHours(double.Parse(_jwtConfig.Expires))
                    : DateTime.Now.AddMinutes(double.Parse(expires)),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return new JwtToken
            {
                Token = jwtToken,
                Issued = DateTime.Now,
                Expires = tokenDescriptor.Expires
            };

        }

        Task<JwtToken> IJWTAuthenticator.GenerateJwtToken(AppUser user, string expires, List<Claim> additionalClaims)
        {
            throw new NotImplementedException();
        }
    }
}
