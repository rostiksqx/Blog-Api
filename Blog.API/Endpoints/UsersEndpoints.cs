using AutoMapper;
using Blog.API.Contracts;
using Blog.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Endpoints;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("api/auth");
        
        endpoints.MapPost("register", Register);
        
        endpoints.MapPost("login", Login);

        endpoints.MapGet("me", GetMe)
            .RequireAuthorization();
        
        endpoints.MapPost("update-password", UpdatePassword)
            .RequireAuthorization();

        endpoints.MapPost("update-email", UpdateEmail)
            .RequireAuthorization();

        endpoints.MapDelete("deleteMe", DeleteUser)
            .RequireAuthorization();

        endpoints.MapGet("{userId:guid}", GetUser)
            .RequireAuthorization();
        
        return endpoints;
    }

    private static async Task<IResult> GetMe(UsersService usersService, HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        
        var user = await usersService.GetUser(token);
        
        return Results.Ok(user);
    }

    private static async Task<IResult> Register(RegisterUserRequest request, UsersService usersService)
    {
        await usersService.Register(request.Username, request.Email, request.Password);
        
        return Results.Ok();
    }

    private static async Task<IResult> Login(LoginUserRequest request, UsersService usersService, HttpContext context)
    {
        var token = await usersService.Login(request.EmailOrUsername, request.Password);
        
        context.Response.Cookies.Append("cookies", token);
        
        return Results.Ok(new { Token = token });
    }
    
    private static async Task<IResult> UpdatePassword(UpdatePasswordRequest request, UsersService usersService, HttpContext context)
    {
        var token = context.Request.Cookies["cookies"] ?? string.Empty;
        
        await usersService.UpdatePassword(token ,request.Password, request.NewPassword);
        
        return Results.Ok("Password updated");
    }

    private static async Task<IResult> UpdateEmail(UpdateEmailRequest request, UsersService usersService)
    {
        await usersService.UpdateEmail(request.Email, request.NewEmail, request.Password);
        
        return Results.Ok("Email updated");
    }
    
    private static async Task<IResult> DeleteUser(UsersService usersService, HttpContext context)
    {
        var token = context.Request.Cookies["cookies"] ?? string.Empty;
        
        await usersService.DeleteUser(token);
        
        context.Response.Cookies.Delete("cookies");
        
        return Results.Ok("User deleted");
    }

    private static async Task<IResult> GetUser(Guid userId, UsersService usersService)
    {
        var user = await usersService.GetUser(userId);
        
        return Results.Ok(user);
    }
}