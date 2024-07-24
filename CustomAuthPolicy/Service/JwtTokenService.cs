using CustomAuthPolicy.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CustomAuthPolicy.Service
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JWTSettings _jwtSettings;
    

        public JwtTokenService(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public JwtSecurityToken GenerateJWToken(User user)
        {
            var claims = new List<Claim>
            { 
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("uid", user.Id.ToString()),
                new Claim("user_name", user.UserName),

            };
          
            // Add roles as claims
            foreach (var role in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
            }
            return JWTGeneration(claims);
        }

        private JwtSecurityToken JWTGeneration(IEnumerable<Claim> claims)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(50),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
            
        }
       
    }
}
