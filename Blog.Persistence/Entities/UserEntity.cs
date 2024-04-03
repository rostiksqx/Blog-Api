namespace Blog.Persistence.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    
    public string Username { get; set; }
    
    public string Email { get; set; }
    
    public string PasswordHash { get; set; }
    
    public List<PostEntity> Posts { get; set; } 
}