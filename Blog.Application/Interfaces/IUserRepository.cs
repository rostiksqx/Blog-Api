using Blog.Core.Models;

namespace Blog.Persistence.Repositories;

public interface IUserRepository
{
    Task Add(User user);
    
    Task<User> GetByEmail(string email);
    
    Task<User> GetUser(Guid id);
    
    Task PromoteToAdmin(Guid id);
    
    Task UpdatePassword(Guid id, string newPassword);
}