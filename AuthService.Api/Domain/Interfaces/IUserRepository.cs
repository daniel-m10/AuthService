using AuthService.Api.Domain.Entities;

namespace AuthService.Api.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> IsEmailTakenAsync(string email);
        Task CreateUserAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
    }
}
