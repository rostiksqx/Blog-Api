using Blog.API.Contracts;
using Blog.Application.Services;

namespace Blog.API.Endpoints;

public static class PostEndpoints
{
    public static IEndpointRouteBuilder MapPostEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("api/posts")
            .RequireAuthorization("UserPolicy");

        endpoints.MapPost("/", CreatePost);

        endpoints.MapGet("/", GetAllPosts);
        
        endpoints.MapGet("{postId:guid}", GetPost);
        
        endpoints.MapDelete("/{postId:guid}", DeletePost);
        // TODO: Add logic and functionality to update post (only owner can update post)
        // endpoints.MapPut("/{id:guid}", UpdatePost);
        
        return endpoints;
    }
    
    private static async Task<IResult> CreatePost(PostRequest request, PostsService postsService, HttpContext context)
    {
        var token = context.Request.Cookies["cookies"] ?? string.Empty;
        
        await postsService.Add(request.Title, request.Content, token);
        
        return Results.Ok("Post created");
    }
    
    private static async Task<IResult> GetPost(Guid postId, PostsService postsService)
    {
        var post = await postsService.Get(postId);
        
        return Results.Ok(post);
    }
    
    private static async Task<IResult> GetAllPosts(PostsService postsService)
    {
        var posts = await postsService.GetAll();
        
        return Results.Ok(posts);
    }
    
    // TODO: Add logic to delete post (only owner can delete post)
    private static async Task<IResult> DeletePost(Guid postId, PostsService postsService, HttpContext context)
    {
        var token = context.Request.Cookies["cookies"] ?? string.Empty;

        await postsService.Delete(postId, token);
        
        return Results.Ok("Post deleted");
    }
}