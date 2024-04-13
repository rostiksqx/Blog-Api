using System.ComponentModel.DataAnnotations;

namespace Blog.API.Contracts;

public record UpdatePasswordRequest(
    [Required] string Email,
    [Required] string Password,
    [Required] string NewPassword);