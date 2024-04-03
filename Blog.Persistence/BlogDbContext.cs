using Blog.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Persistence;

public class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options){}
    
    public DbSet<UserEntity> Users { get; set; }
    
    public DbSet<PostEntity> Posts { get; set; }
}