using Blog.API.Contracts;
using Blog.Application.Services;

namespace Blog.API.Endpoints;

public static class PostEndpoints
{
    public static IEndpointRouteBuilder MapPostEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("api");
        
        endpoints.MapPost("posts", CreatePost);
        
        endpoints.MapGet("posts", GetAllPosts);
        
        endpoints.MapGet("posts/{id}", GetPost);
        
        return endpoints;
    }
    
    private static async Task<IResult> CreatePost(PostRequest request, PostsService postsService, HttpContext context)
    {
        var token = context.Request.Cookies["cookies"];
        
        await postsService.Add(request.Title, request.Content, token);
        
        return Results.Ok("Post created");
    }
    
    private static async Task<IResult> GetPost(Guid id, PostsService postsService)
    {
        var post = await postsService.Get(id);
        
        return Results.Ok(post);
    }
    
    private static async Task<IResult> GetAllPosts(PostsService postsService)
    {
        var posts = await postsService.GetAll();
        
        return Results.Ok(posts);
    }
}