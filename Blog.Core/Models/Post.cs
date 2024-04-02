namespace AuthCookies.Core.Models;

public class Post
{
    private Post(Guid id, string title, string content)
    {
        Id = id;
        Title = title;
        Content = content;
    }
    
    public Guid Id { get;}
    
    public string Title { get;  } = String.Empty;
    
    public string Content { get; } = String.Empty;

    public static (Post post, string error) Create(Guid id, string title, string content)
    {
        var error = string.Empty;

        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
        {
            error = "Title and content are required";
        }

        var post = new Post(id, title, content);
        
        return (post, error);
    }
}