using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blog.Application.Interfaces;
using Blog.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Infrastructure;

public class JwtProvider : IJwtProvider
{
    // TODO: Environment variables for security
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }
    
    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim("userId", user.Id.ToString()),
            new Claim("username", user.Username),
            new Claim("role", user.Role)
        };
        
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_options.ExpiresHours));

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        
        return tokenValue;
    }
    
    public UserClaims GetClaims(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var tokenS = handler.ReadToken(token) as JwtSecurityToken;

        return new UserClaims(
            tokenS!.Claims.First(claim => claim.Type == "userId").Value ?? string.Empty,
            tokenS!.Claims.First(claim => claim.Type == "username").Value ?? string.Empty,
            tokenS!.Claims.First(claim => claim.Type == "role").Value ?? string.Empty);
    }
}