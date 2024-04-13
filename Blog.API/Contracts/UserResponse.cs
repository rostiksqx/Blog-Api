using Blog.Core.Models;

namespace Blog.API.Contracts;

public class UserResponse
{
    public UserResponse(Guid id, string username, string email, string role)
    {
        Id = id;
        Username = username;
        Email = email;
        Role = role;
    }
    
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}