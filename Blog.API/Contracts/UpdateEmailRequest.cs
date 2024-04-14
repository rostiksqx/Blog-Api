using System.ComponentModel.DataAnnotations;

namespace Blog.API.Contracts;

public record UpdateEmailRequest(
    [Required] string Email,
    [Required] string NewEmail,
    [Required] string Password);