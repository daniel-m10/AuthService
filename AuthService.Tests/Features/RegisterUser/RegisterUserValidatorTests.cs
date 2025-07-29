using AuthService.Api.Features.RegisterUser;
using FluentValidation.TestHelper;

namespace AuthService.Tests.Features.RegisterUser
{
    [TestFixture]
    internal class RegisterUserValidatorTests
    {
        private RegisterUserValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new RegisterUserValidator();
        }

        [Test]
        public void Validate_ShouldNotReturnError_WhenEmailAndPasswordAreValid()
        {
            // Arrange
            var request = new RegisterUserRequest
            {
                Email = "test@mail.com",
                Password = "123456"
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
            var request = new RegisterUserRequest
            {
                Email = string.Empty,
                Password = "123456"
            };
            // Act
            var result = _validator.TestValidate(request);
            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("'Email' must not be empty.");
        }

        [Test]
        public void Validate_ShouldReturnError_WhenEmailIsNull()
        {
            // Arrange
            var request = new RegisterUserRequest
            {
                Email = null!,
                Password = "123456"
            };
            // Act
            var result = _validator.TestValidate(request);
            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("'Email' must not be null.");
        }

        [Test]
        public void Validate_ShouldReturnError_WhenEmailIsInvalid()
        {
            // Arrange
            var request = new RegisterUserRequest
            {
                Email = "invalid-email",
                Password = "123456"
            };
            // Act
            var result = _validator.TestValidate(request);
            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("Email format is invalid.");
        }

        [Test]
        public void Validate_ShouldReturnError_WhenPasswordIsEmpty()
        {
            // Arrange
            var request = new RegisterUserRequest
            {
                Email = "test@mail.com",
                Password = string.Empty
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("'Password' must not be empty.");
        }

        [Test]
        public void Validate_ShouldReturnError_WhenPasswordIsNull()
        {
            // Arrange
            var request = new RegisterUserRequest
            {
                Email = "test@mail.com",
                Password = null!
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("'Password' must not be null.");
        }

        [Test]
        public void Validate_ShouldReturnError_WhenPasswordIsTooShort()
        {
            // Arrange
            var request = new RegisterUserRequest
            {
                Email = "test@mail.com",
                Password = "12345"
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("Password is required and must be at least 6 characters long.");
        }

        [Test]
        public void Validate_ShouldReturnError_WhenEmailAndPasswordAreBothInvalid()
        {
            // Arrange
            var request = new RegisterUserRequest
            {
                Email = "invalid-email",
                Password = "12345"
            };
            // Act
            var result = _validator.TestValidate(request);
            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("Email format is invalid.");
            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("Password is required and must be at least 6 characters long.");
        }
    }
}
