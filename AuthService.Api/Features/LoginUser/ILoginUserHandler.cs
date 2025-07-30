using AuthService.Api.Domain.Common;

namespace AuthService.Api.Features.LoginUser
{
    public interface ILoginUserHandler
    {
        Task<Result<string>> HandleAsync(LoginUserRequest request);
    }
}
