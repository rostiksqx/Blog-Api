using System.ComponentModel.DataAnnotations;

namespace AuthCookies.API.Contracts;

public record LoginUserRequest(
    [Required]string Email,
    [Required]string Password);