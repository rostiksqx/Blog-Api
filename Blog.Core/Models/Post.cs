namespace Blog.Core.Models;

public class Post
{
    public Post(Guid id, string title, string content, Guid userId)
    {
        Id = id;
        Title = title;
        Content = content;
        UserId = userId;
    }
    
    public Guid Id { get;}
    
    public string Title { get;  } = String.Empty;
    
    public string Content { get; } = String.Empty;
    
    public Guid UserId { get; }

    public static Post Create(Guid id, string title, string content, Guid userId)
    {
        var error = string.Empty;

        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
        {
            error = "Title and content are required";
        }

        var post = new Post(id, title, content, userId);

        return post;
    }
}