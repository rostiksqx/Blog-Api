using Blog.Application.Interfaces;
using Blog.Core.Models;
using Blog.Infrastructure;
using Blog.Persistence.Repositories;

namespace Blog.Application.Services;

public class PostsService
{
    private readonly IPostRepository _postRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IUserRepository _userRepository;

    public PostsService(IPostRepository postRepository, IJwtProvider jwtProvider, IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _jwtProvider = jwtProvider;
        _userRepository = userRepository;
    }
    
    public async Task<Post> Add(string title, string content, string token)
    {
        var userClaims = _jwtProvider.GetClaims(token);

        var userId = userClaims.UserId;
        
        var post = Post.Create(Guid.NewGuid(), title, content, Guid.Parse(userId), userClaims.Username);
        
        await _postRepository.Add(post, Guid.Parse(userId));
        
        return post;
    }
    
    public async Task<Post> Get(Guid id)
    {
        return await _postRepository.Get(id);
    }
    
    public async Task<IEnumerable<Post>> GetAll()
    {
        return await _postRepository.GetAll();
    }
    
    public async Task Delete(Guid id, string token)
    {
        var post = await _postRepository.Get(id);

        var userClaims = _jwtProvider.GetClaims(token);

        if (post.UserId != Guid.Parse(userClaims.UserId))
        {
            throw new Exception("You are not owner");
        }

        await _postRepository.Delete(id);
    }

    public async Task<Post> Update(Guid id, string title, string content, string token)
    {
        var postToUpdate = await _postRepository.Get(id);

        var userClaims = _jwtProvider.GetClaims(token);

        if (postToUpdate.UserId != Guid.Parse(userClaims.UserId))
        {
            throw new Exception("You are not owner");
        }

        var post = await _postRepository.Update(id, title, content);

        return post;
    }
}