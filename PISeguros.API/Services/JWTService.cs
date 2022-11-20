using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PISeguros.API.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PISeguros.API.Services
{
    public class JWTService
    {
        private readonly JWTSettings _options;

        public JWTService(IOptions<JWTSettings> options)
        {
            _options = options.Value;
        }

        public TokenResult CreateToken(ClaimsIdentity claims)
        {
            var key = Encoding.UTF8.GetBytes(_options.IssuerSigningKey);
            var securityKey = new SymmetricSecurityKey(key);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _options.Issuer,
                Audience = _options.Audience,
                Subject = claims,
                Expires = DateTime.UtcNow.AddSeconds(_options.TokenExpiration),
                SigningCredentials = signingCredentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenResult
            {
                Token = tokenHandler.WriteToken(token),
                Expires = tokenDescriptor.Expires
            };
        }

        public string GetUserName(string token)
        {
            var key = Encoding.UTF8.GetBytes(_options.IssuerSigningKey);
            var handler = new JwtSecurityTokenHandler();

            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidAudience = _options.Audience,
                ValidIssuer = _options.Issuer
            };
            var claims = handler.ValidateToken(token, validations, out _);

            var identity = claims.Identity as ClaimsIdentity;
            return identity.Claims.First(x => x.Type == ClaimTypes.Name).Value;
        }
    }
}
