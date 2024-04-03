using Blog.Core.Models;

namespace Blog.Persistence.Repositories;

public interface IUserRepository
{
    Task Add(User user);
    Task<User> GetByEmail(string email);
}