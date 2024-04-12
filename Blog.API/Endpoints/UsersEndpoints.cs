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

        endpoints.MapGet("me", GetMe);
            // .RequireAuthorization();
        
        endpoints.MapGet("users/{userId:guid}/promote", PromoteUser)
            .RequireAuthorization("SuperAdminPolicy");
        
        return endpoints;
    }

    private static async Task<IResult> GetMe(UsersService usersService, HttpContext context)
    {
        var token = context.Request.Cookies["cookies"];
        
        var userWithPosts = await usersService.GetUser(token);
        
        return Results.Ok(userWithPosts);
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
    
    private static async Task<IResult> PromoteUser(Guid userId, UsersService usersService)
    {
        await usersService.PromoteToAdmin(userId);
        
        return Results.Ok();
    }
}