using AuthService.Api.Domain.Common;
using AuthService.Api.Domain.Entities;
using AuthService.Api.Features.RegisterUser;

namespace AuthService.Api.Domain.Interfaces
{
    public interface IRegisterUserHandler
    {
        Task<Result<User>> HandleAsync(RegisterUserRequest request);
    }
}
