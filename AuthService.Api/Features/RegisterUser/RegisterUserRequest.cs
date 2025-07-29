namespace AuthService.Api.Features.RegisterUser
{
    public sealed class RegisterUserRequest
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
