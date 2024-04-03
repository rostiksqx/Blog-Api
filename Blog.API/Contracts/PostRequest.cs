using Microsoft.Build.Framework;

namespace Blog.API.Contracts;

public record PostRequest(
    string Title, 
    string Content);