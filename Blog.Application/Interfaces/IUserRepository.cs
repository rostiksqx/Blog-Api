using Blog.Core.Models;

namespace Blog.Persistence.Repositories;

public interface IUserRepository
{
    Task Add(User user);
    
    Task<User> GetByEmail(string email);
    
    Task<UserResponse> GetUser(Guid id);
    
    Task PromoteToAdmin(Guid id);
}