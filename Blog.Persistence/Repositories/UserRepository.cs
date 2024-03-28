using AuthCookies.Core.Models;
using AuthCookies.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthCookies.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _dbContext;

    public UserRepository(UserDbContext dbContext)
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
            PasswordHash = user.PasswordHash
        };
        
        await _dbContext.Users.AddAsync(userEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User> GetByEmail(string email)
    {
        var userEntity = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email) ?? throw new Exception("User not found");

        return new User(userEntity.Id, userEntity.Username, userEntity.Email, userEntity.PasswordHash);
    }
}