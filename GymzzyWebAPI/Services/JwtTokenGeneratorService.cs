using GymzzyWebAPI.Models;
using GymzzyWebAPI.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GymzzyWebAPI.Services
{
    public class JwtTokenGeneratorService : ITokenGeneratorService
    {
        private readonly IConfigurationSection _jwtTokenSection;

        public JwtTokenGeneratorService(IConfiguration configuration)
        {
            _jwtTokenSection = configuration.GetSection("JwtTokenOptions");
        }

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            _jwtTokenSection.GetValue<string>("IssuerSigningKey")));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            };

            var token = new JwtSecurityToken(issuer: _jwtTokenSection.GetValue<string>("ValidIssuer"),
              audience: _jwtTokenSection.GetValue<string>("ValidAudience"),
              claims: claims,
              notBefore: System.DateTime.Now,
              expires: System.DateTime.Now.AddSeconds(_jwtTokenSection.GetValue<double>("ExpiresInSeconds")),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
