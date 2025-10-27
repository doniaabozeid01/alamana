using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace alamana.Infrastructure.Identity
{
    public class JwtTokenService 
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }



public string GenerateJwtToken(AppUser user, IEnumerable<string>? roles = null)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = _configuration["Jwt:Key"]!;
        var keyBytes = Encoding.UTF8.GetBytes(key);

        var now = DateTime.UtcNow;

        // المدة من الكونفيج (افتراضي 30 دقيقة/يوم حسب اختيارك)
        var minutes = int.TryParse(_configuration["Jwt:AccessTokenMinutes"], out var m) ? m : 30;
        var expires = now.AddMinutes(minutes);

        var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(ClaimTypes.NameIdentifier,   user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
        new Claim(ClaimTypes.Name,               user.UserName ?? user.Email ?? string.Empty),
        new Claim(JwtRegisteredClaimNames.Jti,   Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat,
                  new DateTimeOffset(now).ToUnixTimeSeconds().ToString(),
                  ClaimValueTypes.Integer64),
    };

        // FullName إن لزم
        if (!string.IsNullOrWhiteSpace(user.FullName))
            claims.Add(new Claim("full_name", user.FullName));


        // الأدوار (إن وُجدت)
        if (roles != null)
        {
            foreach (var r in roles.Where(r => !string.IsNullOrWhiteSpace(r)))
                claims.Add(new Claim(ClaimTypes.Role, r));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            NotBefore = now,
            Expires = expires,
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256)
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }





    }
}
