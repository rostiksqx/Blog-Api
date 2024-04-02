using Microsoft.Build.Framework;

namespace AuthCookies.API.Contracts;

public record PostRequest(
    string Title, 
    string Content);