using AuthCookies.API.Contracts;
using AuthCookies.Application.Services;

namespace AuthCookies.API.UserEndpoints;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("api");
        
        endpoints.MapPost("register", Register);
        
        endpoints.MapPost("login", Login);
        
        endpoints.MapGet("me", GetMe)
            .RequireAuthorization();

        return endpoints;
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