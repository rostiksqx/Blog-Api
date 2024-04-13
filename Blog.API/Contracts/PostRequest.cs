using System.ComponentModel.DataAnnotations;

namespace Blog.API.Contracts;

public record PostRequest(
    [Required] string Title, 
    [Required] string Content);