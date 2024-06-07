using Blog.Core.Models;

namespace Blog.Persistence.Repositories;

public interface IUserRepository
{
    Task Add(User user);
    
    Task<User> GetByEmail(string email);
    
    Task<User> GetByUsername(string username);
    
    Task<User> GetUser(Guid id);
    
    Task UpdatePassword(Guid id, string newPassword);

    Task UpdateEmail(Guid id, string newEmail);

    Task Delete(Guid id);
}