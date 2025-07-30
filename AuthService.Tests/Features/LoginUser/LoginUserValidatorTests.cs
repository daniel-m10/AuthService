using AuthService.Api.Features.LoginUser;
using FluentValidation.TestHelper;

namespace AuthService.Tests.Features.LoginUser
{
    [TestFixture]
    internal class LoginUserValidatorTests
    {
        private LoginUserValidator _validator = null!;

        [SetUp]
        public void Setup()
        {
            _validator = new LoginUserValidator();
        }

        [Test]
        public void Validate_ShouldPass_WhenEmailAndPasswordAreValid()
        {
            // Arrange
            var request = new LoginUserRequest
            {
                Email = "user@mail.com",
                Password = "ValidPassword123"
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void Validate_ShouldReturnError_WhenEmailIsEmpty()
        {
            // Arrange
            var request = new LoginUserRequest
            {
                Email = string.Empty,
                Password = "ValidPassword123"
            };
            // Act
            var result = _validator.TestValidate(request);
            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email).WithErrorMessage("Email is required.");
        }

        [Test]
        public void Validate_ShouldReturnError_WhenEmailIsInvalid()
        {
            // Arrange
            var request = new LoginUserRequest
            {
                Email = "invalid-email",
                Password = "ValidPassword123"
            };
            // Act
            var result = _validator.TestValidate(request);
            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email).WithErrorMessage("Email format is invalid.");
        }

        [Test]
        public void Validate_ShouldReturnError_WhenPasswordIsEmpty()
        {
            // Arrange
            var request = new LoginUserRequest
            {
                Email = "user@mail.com",
                Password = string.Empty
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage("Password is required.");
        }

        [Test]
        public void Validate_ShouldReturnError_WhenEmailAndPasswordAreEmpty()
        {
            // Arrange
            var request = new LoginUserRequest
            {
                Email = string.Empty,
                Password = string.Empty
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email).WithErrorMessage("Email is required.");
            result.ShouldHaveValidationErrorFor(x => x.Password).WithErrorMessage("Password is required.");
        }
    }
}
