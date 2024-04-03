using Blog.Core.Models;

namespace Blog.Infrastructure;

public interface IJwtProvider
{
    string GenerateToken(User user);
    
    string GetUserId(string token);
}