using AuthService.Api.Domain.Common;
using AuthService.Api.Domain.Entities;
using AuthService.Api.Domain.Interfaces;

namespace AuthService.Api.Features.RegisterUser
{
    public class RegisterUserHandler(IUserRepository userRepository) : IRegisterUserHandler
    {

        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result<User>> HandleAsync(RegisterUserRequest request)
        {
            if (await _userRepository.IsEmailTakenAsync(request.Email))
                return Result<User>.Failure("Email is already taken.");

            var user = new User
            {
                Email = request.Email,
                PasswordHash = request.Password // In a real application, ensure to hash the password
            };

            await _userRepository.CreateUserAsync(user);

            return Result<User>.Success(user);
        }
    }
}
