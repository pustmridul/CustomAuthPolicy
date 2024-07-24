using CustomAuthPolicy.Data;
using System.IdentityModel.Tokens.Jwt;

namespace CustomAuthPolicy.Service
{
    public interface IJwtTokenService
    {
        JwtSecurityToken GenerateJWToken(User user);
    }
}
