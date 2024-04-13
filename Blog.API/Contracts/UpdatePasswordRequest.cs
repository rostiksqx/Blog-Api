using System.ComponentModel.DataAnnotations;

namespace Blog.API.Contracts;

public record UpdatePasswordRequest(
    [Required] string Password,
    [Required] string NewPassword);