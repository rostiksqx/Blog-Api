using System.ComponentModel.DataAnnotations;

namespace Blog.API.Contracts;

public record LoginUserRequest(
    [Required] string Email,
    [Required] string Password);