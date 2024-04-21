namespace Blog.Persistence.Entities;

public class PostEntity
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    
    public string Content { get; set; }

    public Guid UserId { get; set; }
    
    public UserEntity? User { get; set; }
    
    public int ViewsCount { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}