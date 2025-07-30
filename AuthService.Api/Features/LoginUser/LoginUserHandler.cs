
using AuthService.Api.Domain.Common;
using AuthService.Api.Domain.Interfaces;

namespace AuthService.Api.Features.LoginUser
{
    public class LoginUserHandler(IUserRepository userRepository) : ILoginUserHandler
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result<string>> HandleAsync(LoginUserRequest request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            if (user is null)
                return Result<string>.Failure("User does not exist"); ;

            if (user.PasswordHash != request.Password)
                return Result<string>.Failure("Incorrect password");

            var token = $"fake-jwt-for-{user.Email}";

            return Result<string>.Success(token);
        }
    }
}
