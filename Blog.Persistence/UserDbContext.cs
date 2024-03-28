using AuthCookies.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthCookies.Persistence;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options){}
    
    public DbSet<UserEntity> Users { get; set; }
}