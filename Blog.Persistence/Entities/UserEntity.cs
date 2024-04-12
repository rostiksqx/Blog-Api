namespace Blog.Persistence.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    
    public string Username { get; set; }
    
    public string Email { get; set; }
    
    public string PasswordHash { get; set; }
    
    public string Role { get; set; } = "user";
    
    public List<PostEntity> Posts { get; set; } 
}