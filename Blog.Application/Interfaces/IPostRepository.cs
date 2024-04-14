using Blog.Core.Models;

namespace Blog.Persistence.Repositories;

public interface IPostRepository
{
    Task Add(Post post, Guid userId);
    
    Task<Post> Get(Guid id);
    
    Task<IEnumerable<Post>> GetAll();
    
    Task Delete(Guid id);
}