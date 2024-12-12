using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SalifyCRM.IdentityServer.Domain;
using SalifyCRM.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application
{
    public class JwtHelper : ITokenHelper
    {
        private readonly TokenOptions _tokenOptions;

        public JwtHelper(IOptions<TokenOptions> tokenOptions)
        {
            _tokenOptions = tokenOptions.Value;
        }

        // Access token oluşturma
        public string GenerateAccessToken(string email, List<OperationClaim> roles)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, email),
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration),  // Access Token geçerliliği
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Refresh token oluşturma
        public string GenerateRefreshToken(string email)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, email)  // Email bilgisini claim olarak ekliyoruz
    };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_tokenOptions.RefreshTokenExpiration),  // Refresh token geçerliliği
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        // Access token doğrulama
        public ClaimsPrincipal ValidateAccessToken(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = _tokenOptions.Issuer,
                ValidAudience = _tokenOptions.Audience,
                IssuerSigningKey = securityKey
            };

            try
            {
                return tokenHandler.ValidateToken(token, validationParameters, out _);
            }
            catch
            {
                return null;
            }
        }

        // Refresh token doğrulama
        public string ValidateRefreshToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var jwtToken = tokenHandler.ReadJwtToken(token);  // Token'ı çözümle

                // Email bilgisini claim'den al
                var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

                if (emailClaim != null)
                {
                    return emailClaim.Value;  // Email bilgisi başarıyla alındı
                }

                return null;  // Email bulunamadı
            }
            catch (Exception)
            {
                return null;  // Token geçerli değilse veya hata oluşursa null döndür
            }
        }

    }
}