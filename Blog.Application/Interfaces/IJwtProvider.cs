using AuthCookies.Core.Models;

namespace AuthCookies.Infrastructure;

public interface IJwtProvider
{
    string GenerateToken(User user);
}