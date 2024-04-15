namespace Blog.Persistence.Repositories;

public interface IAdminRepository
{
    Task PromoteToAdmin(Guid id);
}