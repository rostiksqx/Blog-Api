using AuthCookies.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthCookies.Persistence;

public class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options){}
    
    public DbSet<UserEntity> Users { get; set; }
    
    public DbSet<PostEntity> Posts { get; set; }
}