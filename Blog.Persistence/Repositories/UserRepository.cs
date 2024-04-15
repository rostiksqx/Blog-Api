using Blog.Core.Models;
using Blog.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BlogDbContext _dbContext;

    public UserRepository(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(User user)
    {
        var userEntity = new UserEntity
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            Role = user.Role,
        };
        
        await _dbContext.Users.AddAsync(userEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User> GetByEmail(string email)
    {
        var userEntity = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email) ?? throw new Exception("User not found");

        return new User(userEntity.Id, userEntity.Username, userEntity.Email, userEntity.PasswordHash, userEntity.Role, []);
    }

    public async Task<User> GetUser(Guid id)
    {
        var userEntity = await _dbContext.Users
            .AsNoTracking()
            .Include(x => x.Posts)
            .FirstOrDefaultAsync(u => u.Id == id) ?? throw new Exception("User not found");

        return new User(userEntity.Id, userEntity.Username, userEntity.Email, userEntity.PasswordHash, userEntity.Role, userEntity.Posts.Select(p => new Post(p.Id, p.Title, p.Content, p.UserId)).ToList());
    }

    public async Task UpdatePassword(Guid id, string newPassword)
    {
        var userEntity = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == id) ?? throw new Exception("User not found");

        userEntity.PasswordHash = newPassword;
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateEmail(Guid id, string newEmail)
    {
        var userEntity = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == id) ?? throw new Exception("User not found");

        userEntity.Email = newEmail;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var userEntity = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == id) ?? throw new Exception("User not found");
        
        _dbContext.Users.Remove(userEntity);
        await _dbContext.SaveChangesAsync();
    }
}