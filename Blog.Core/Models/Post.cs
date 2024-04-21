namespace Blog.Core.Models;

public class Post
{
    public Post(Guid id, string title, string content, Guid userId, int viewCount)
    {
        Id = id;
        Title = title;
        Content = content;
        UserId = userId;
        ViewCount = viewCount;
    }
    
    public Guid Id { get;}
    
    public string Title { get;  } = String.Empty;
    
    public string Content { get; } = String.Empty;
    
    public Guid UserId { get; }
    
    public int ViewCount { get; set; }
    
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    // TODO: Save image to database or add local storage
    // public byte[] Image { get; set; }

    public static Post Create(Guid id, string title, string content, Guid userId, int viewCount = 0)
    {
        var error = string.Empty;

        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
        {
            error = "Title and content are required";
        }
        
        if (!string.IsNullOrEmpty(error))
        {
            throw new ArgumentException(error);
        }

        var post = new Post(id, title, content, userId, viewCount);

        return post;
    }
}