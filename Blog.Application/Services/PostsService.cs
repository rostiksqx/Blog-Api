using AuthCookies.Core.Models;
using AuthCookies.Persistence.Repositories;

namespace AuthCookies.Application.Services;

public class PostsService
{
    private readonly IPostRepository _postRepository;

    public PostsService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    
    public async Task Add(string title, string content)
    {
        var post = Post.Create(Guid.NewGuid(), title, content);
        
        await _postRepository.Add(post);
    }
    
    public async Task GetAll()
    {
        await _postRepository.GetAll();
    }
    
    public async Task Get(Guid id)
    {
        await _postRepository.Get(id);
    }
}