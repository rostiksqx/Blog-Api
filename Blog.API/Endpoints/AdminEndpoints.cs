using Blog.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Endpoints;

public static class AdminEndpoints
{
    public static IEndpointRouteBuilder MapAdminsEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("api/admin")
            .RequireAuthorization("AdminPolicy");
        
        endpoints.MapPost("{userId:guid}/promote", PromoteUser)
            .RequireAuthorization("SuperAdminPolicy");
        
        endpoints.MapDelete("{userId:guid}/deleteUser", DeleteUser);
        
        endpoints.MapDelete("{postId:guid}/deletePost", DeletePost);
        
        return endpoints;
    }

    private static async Task<IResult> PromoteUser(Guid userId, AdminsService adminsService)
    {
        await adminsService.PromoteToAdmin(userId);
        
        return Results.Ok("User promoted");
    }
    
    private static async Task<IResult> DeleteUser(Guid userId, AdminsService adminsService)
    {
        await adminsService.DeleteUser(userId);
        
        return Results.Ok("User deleted");
    }
    
    private static async Task<IResult> DeletePost(Guid postId, AdminsService adminsService)
    {
        await adminsService.DeletePost(postId);
        
        return Results.Ok("Post deleted");
    }
}