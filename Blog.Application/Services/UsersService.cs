using Blog.Core.Models;
using Blog.Infrastructure;
using Blog.Persistence.Repositories;

namespace Blog.Application.Services;

public class UsersService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public UsersService(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }
    
    public async Task Register(string username, string email, string password)
    {
        var hashPassword = _passwordHasher.Generate(password);
        
        var user = User.Create(Guid.NewGuid(), username, email, hashPassword, "user");
        
        await _userRepository.Add(user);
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _userRepository.GetByEmail(email);
        
        var result = _passwordHasher.Verify(password, user.PasswordHash);

        if (result == false)
        {
            throw new Exception("Failed to login");
        }

        var token = _jwtProvider.GenerateToken(user);
        
        return token;
    }
    
    public async Task<UserResponse> GetUser(string token)
    {
        var userId = _jwtProvider.GetUserId(token);
        
        var user = await _userRepository.GetUser(Guid.Parse(userId));
        
        var userResponse = new UserResponse(user.Id, user.Username, user.Email, user.Role, user.Posts);
        
        return userResponse;
    }

    public async Task<UserResponse> GetUser(Guid id)
    {
        var user = await _userRepository.GetUser(id);
        
        var userResponse = new UserResponse(user.Id, user.Username, user.Email, user.Role, user.Posts);
        
        return userResponse;
    }

    public async Task UpdatePassword(string token, string password, string newPassword)
    {
        var userId = _jwtProvider.GetUserId(token);
        
        var user = await _userRepository.GetUser(Guid.Parse(userId));
        
        if (!_passwordHasher.Verify(password, user.PasswordHash))
        {
            throw new Exception("Wrong password");
        }
        
        var hashPassword = _passwordHasher.Generate(newPassword);
        
        await _userRepository.UpdatePassword(Guid.Parse(userId), hashPassword);
    }

    public async Task UpdateEmail(string email, string newEmail, string password)
    {
        var user = await _userRepository.GetByEmail(email);
        
        if (!_passwordHasher.Verify(password, user.PasswordHash))
        {
            throw new Exception("Wrong password");
        }
        
        await _userRepository.UpdateEmail(user.Id, newEmail);
    }

    public async Task DeleteUser(string token)
    {
        var userId = _jwtProvider.GetUserId(token);
        
        await _userRepository.Delete(Guid.Parse(userId));
    }
}