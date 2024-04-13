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
        
        return user;
    }

    public async Task PromoteToAdmin(Guid id)
    {
        await _userRepository.PromoteToAdmin(id);
    }

    public async Task UpdatePassword(string email, string password, string newPassword)
    {
        var user = await _userRepository.GetByEmail(email);
        
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        if (!_passwordHasher.Verify(password, user.PasswordHash))
        {
            throw new Exception("Wrong password");
        }
        
        var hashPassword = _passwordHasher.Generate(newPassword);
        
        await _userRepository.UpdatePassword(email, hashPassword);
    }
}