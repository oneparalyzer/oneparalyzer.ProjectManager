using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using oneparalyzer.ProjectManager.Auth.Api.Entities;
using oneparalyzer.ProjectManager.Auth.Api.Helpers.Interfaces;
using oneparalyzer.ProjectManager.Auth.Api.Options;

namespace oneparalyzer.ProjectManager.Auth.Api.Helpers.Implementations;

public sealed class JwtTokenHelper : IJwtTokenHelper
{
    private readonly JwtTokenOptions _jwtOptions;

    public JwtTokenHelper(IOptions<JwtTokenOptions> options)
    {
        _jwtOptions = options.Value;
    }

    public string GenerateToken(User user, IList<string> roles)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        List<Claim> claims = GetClaims(user, roles);

        var securityToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.LifeTime),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    private List<Claim> GetClaims(User user, IList<string> roles)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }
}
