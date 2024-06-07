using Blog.Core.Models;

namespace Blog.Application.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(User user);

    UserClaims GetClaims(string token);
}