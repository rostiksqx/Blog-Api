namespace AuthCookies.Core.Models;

public class User
{
    public User(Guid id, string username, string email, string password)
    {
        Id = id;
        Username = username;
        Email = email;
        PasswordHash = password;
    }
    
    public Guid Id { get; set; }
    
    public string Username { get; set; }
    
    public string Email { get; set; }
    
    public string PasswordHash { get; set; }

    public static User Create(Guid id, string username, string email, string password)
    {
        return new User(id, username, email, password);
    }
}