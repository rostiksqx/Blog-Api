namespace Blog.API.Contracts;

public record RegisterUserResponse(
    Guid Id,
    string Username,
    string Email,
    string Role);