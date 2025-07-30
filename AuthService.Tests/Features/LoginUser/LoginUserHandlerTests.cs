using AuthService.Api.Domain.Entities;
using AuthService.Api.Domain.Interfaces;
using AuthService.Api.Features.LoginUser;
using FluentAssertions;
using NSubstitute;

namespace AuthService.Tests.Features.LoginUser
{
    [TestFixture]
    internal class LoginUserHandlerTests
    {
        private IUserRepository _userRepository = null!;
        private LoginUserHandler _handler = null!;

        [SetUp]
        public void Setup()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new LoginUserHandler(_userRepository);
        }

        [Test]
        public async Task HandleAsync_ShouldReturnToken_WhenCredentialsAreCorrect()
        {
            // Arrange
            var request = new LoginUserRequest
            {
                Email = "user@mail.com",
                Password = "ValidPassword123"
            };

            var user = new User
            {
                Email = request.Email,
                PasswordHash = "ValidPassword123"
            };

            _userRepository.GetUserByEmailAsync(request.Email)!.Returns(Task.FromResult(user));

            // Act
            var result = await _handler.HandleAsync(request);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNullOrWhiteSpace();
        }

        [Test]
        public async Task HandleAsync_ShouldFail_WhenUserDoesNotExist()
        {
            // Arrange
            var request = new LoginUserRequest
            {
                Email = "unknown@mail.com",
                Password = "any"
            };

            _userRepository.GetUserByEmailAsync(request.Email)!.Returns((User?)null);

            // Act
            var result = await _handler.HandleAsync(request);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("User does not exist");
        }

        [Test]
        public async Task HandleAsync_ShouldFail_WhenPasswordIsIncorrect()
        {
            // Arrange
            var request = new LoginUserRequest
            {
                Email = "user@mail.com",
                Password = "wrong-password"
            };

            var user = new User
            {
                Email = request.Email,
                PasswordHash = "correct-password"
            };

            _userRepository.GetUserByEmailAsync(request.Email)!.Returns(user);

            // Act
            var result = await _handler.HandleAsync(request);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("Incorrect password");
        }
    }
}
