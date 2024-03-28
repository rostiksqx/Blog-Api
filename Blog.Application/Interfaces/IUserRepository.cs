using AuthCookies.Core.Models;

namespace AuthCookies.Persistence.Repositories;

public interface IUserRepository
{
    Task Add(User user);
    Task<User> GetByEmail(string email);
}