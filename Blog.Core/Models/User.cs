namespace Blog.Core.Models;

public class User
{
    public User(Guid id, string username, string email, string password, List<Post> posts)
    {
        Id = id;
        Username = username;
        Email = email;
        PasswordHash = password;
        Posts = posts;
    }
    
    public Guid Id { get; set; }
    
    public string Username { get; set; }
    
    public string Email { get; set; }
    
    public string PasswordHash { get; set; }
    
    public List<Post> Posts { get; set; }

    public static User Create(Guid id, string username, string email, string password)
    {
        return new User(id, username, email, password, new List<Post>());
    }
}