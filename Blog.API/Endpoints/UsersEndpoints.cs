using Blog.API.Contracts;
using Blog.Application.Services;

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
        
        endpoints.MapGet("{userId:guid}/promote", PromoteUser)
            .RequireAuthorization("SuperAdminPolicy");
        
        endpoints.MapPost("update-password", UpdatePassword)
            .RequireAuthorization();
        
        return endpoints;
    }

    private static async Task<IResult> GetMe(UsersService usersService, HttpContext context)
    {
        var token = context.Request.Cookies["cookies"] ?? string.Empty;
        
        var user = await usersService.GetUser(token);
        
        return Results.Ok(user);
    }

    private static async Task<IResult> Register(RegisterUserRequest request, UsersService usersService)
    {
        await usersService.Register(request.Username, request.Email, request.Password);
        
        return Results.Ok("Welcome to Blog!");
    }

    private static async Task<IResult> Login(LoginUserRequest request, UsersService usersService, HttpContext context)
    {
        var token = await usersService.Login(request.Email, request.Password);
        
        context.Response.Cookies.Append("cookies", token);
        
        return Results.Ok("Welcome back!");
    }
    
    private static async Task<IResult> PromoteUser(Guid userId, UsersService usersService)
    {
        await usersService.PromoteToAdmin(userId);
        
        return Results.Ok("User promoted");
    }
    
    private static async Task<IResult> UpdatePassword(UpdatePasswordRequest request, UsersService usersService, HttpContext context)
    {
        var token = context.Request.Cookies["cookies"] ?? string.Empty;
        
        await usersService.UpdatePassword(token ,request.Password, request.NewPassword);
        
        return Results.Ok("Password updated");
    }
}