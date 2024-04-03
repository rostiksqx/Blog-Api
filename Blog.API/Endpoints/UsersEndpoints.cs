using Blog.API.Contracts;
using Blog.Application.Services;
using Blog.Core.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Endpoints;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("api");
        
        endpoints.MapPost("register", Register);
        
        endpoints.MapPost("login", Login);
        
        endpoints.MapGet("me", GetMe)
            .RequireAuthorization();

        endpoints.MapGet("meWithPosts", GetMeWithPosts);
        
        return endpoints;
    }

    private static async Task<IResult> GetMeWithPosts([FromBody] RegisterUserRequest request, [FromServices] UsersService usersService)
    {
        var userWithPosts = await usersService.GetWithPosts(request.Email);
        
        return Results.Ok(userWithPosts);
    }

    private static Task<IResult> GetMe()
    {
        return Task.FromResult(Results.Ok());
    }

    private static async Task<IResult> Register(RegisterUserRequest request, UsersService usersService)
    {
        await usersService.Register(request.Username, request.Email, request.Password);
        
        return Results.Ok();
    }

    private static async Task<IResult> Login(LoginUserRequest request, UsersService usersService, HttpContext context)
    {
        var token = await usersService.Login(request.Email, request.Password);
        
        context.Response.Cookies.Append("cookies", token);
        
        return Results.Ok();
    }
}