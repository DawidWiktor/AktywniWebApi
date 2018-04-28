using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aktywni.Infrastructure.DTO;
using Aktywni.Infrastructure.Extensions;
using Aktywni.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Aktywni.Infrastructure.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSettings _jwtSettings;
        public JwtHandler(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public JwtDTO CreateToken(int userID, string role)
        {
            System.Diagnostics.Debug.WriteLine("token " + userID + " d " + role);
            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userID.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userID.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString())
            };

            var expires = now.AddMinutes(_jwtSettings.ExpiryMinutes);
            var signingCredetials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredetials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            
            return new JwtDTO{
                Token = token,
                Expires = expires.ToTimestamp()
            };
        }
    }
}