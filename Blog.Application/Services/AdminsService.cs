using Blog.Persistence.Repositories;

namespace Blog.Application.Services;

public class AdminsService
{
    private readonly IAdminRepository _adminRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPostRepository _postRepository;

    public AdminsService(IAdminRepository adminRepository, IUserRepository userRepository, IPostRepository postRepository)
    {
        _adminRepository = adminRepository;
        _userRepository = userRepository;
        _postRepository = postRepository;
    }
    
    public async Task PromoteToAdmin(Guid id)
    {
        var user = await _userRepository.GetUser(id);

        switch (user.Role)
        {
            case "admin":
                throw new Exception("User is already admin");
            case "SuperAdmin":
                throw new Exception("You cannot promote this user");
            default:
                await _adminRepository.PromoteToAdmin(id);
                break;
        }
    }
    
    public async Task DeleteUser(Guid id)
    {
        await _userRepository.Delete(id);
    }

    public async Task DeletePost(Guid id)
    {
        await _postRepository.Delete(id);
    }
}