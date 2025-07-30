using AuthService.Api.Domain.Entities;
using AuthService.Api.Domain.Interfaces;

namespace AuthService.Api.Infrastructure
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = [];

        public Task CreateUserAsync(User user)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }

        public Task<bool> IsEmailTakenAsync(string email)
        {
            var exists = _users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(exists);
        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            var user = _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(user);
        }

        public IReadOnlyCollection<User> GetAllUsers() => _users.AsReadOnly();
    }
}
