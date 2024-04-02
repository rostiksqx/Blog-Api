using AuthCookies.Core.Models;

namespace AuthCookies.Persistence.Repositories;

public interface IPostRepository
{
    Task Add(Post post);
    Task<Post> Get(Guid id);
    Task<IEnumerable<Post>> GetAll();
}