namespace Blog.Core.Models;

public class UserResponse
{
    public UserResponse(Guid id, string username, string email, string role, List<Post> posts)
    {
        Id = id;
        Username = username;
        Email = email;
        Role = role;
        Posts = posts;
    }
    
    public Guid Id { get; set; }

    public string Username { get; set; }
    
    public string Email { get; set; }
    
    public string Role { get; set; }
    
    public List<Post> Posts { get; set; }
}