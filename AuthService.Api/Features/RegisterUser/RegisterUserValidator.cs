using FluentValidation;

namespace AuthService.Api.Features.RegisterUser
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("'Email' must not be empty.")
                .NotNull().WithMessage("'Email' must not be null.")
                .EmailAddress().WithMessage("Email format is invalid.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("'Password' must not be empty.")
                .NotNull().WithMessage("'Password' must not be null.")
                .MinimumLength(6).WithMessage("Password is required and must be at least 6 characters long.");
        }
    }
}
