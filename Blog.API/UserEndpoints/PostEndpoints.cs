using AuthCookies.API.Contracts;
using AuthCookies.Application.Services;

namespace AuthCookies.API.UserEndpoints;

public static class PostEndpoints
{
    public static IEndpointRouteBuilder MapPostEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("api");
        
        endpoints.MapPost("posts", CreatePost);
        
        endpoints.MapGet("posts/{id}", GetPost);
        
        endpoints.MapGet("posts", GetAllPosts);
        
        return endpoints;
    }
    
    private static async Task<IResult> CreatePost(PostRequest request, PostsService postsService)
    {
        await postsService.Add(request.Title, request.Content);
        
        return Results.Ok();
    }
    
    private static async Task<IResult> GetPost(Guid id, PostsService postsService)
    {
        await postsService.Get(id);
        
        return Results.Ok();
    }
    
    private static async Task<IResult> GetAllPosts(PostsService postsService)
    {
        await postsService.GetAll();
        
        return Results.Ok();
    }
}