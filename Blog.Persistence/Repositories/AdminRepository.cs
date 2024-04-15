using Microsoft.EntityFrameworkCore;

namespace Blog.Persistence.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly BlogDbContext _dbContext;

    public AdminRepository(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task PromoteToAdmin(Guid id)
    {
        var userEntity = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == id) ?? throw new Exception("User not found");
        
        userEntity.Role = "admin";
        await _dbContext.SaveChangesAsync();
    }
}