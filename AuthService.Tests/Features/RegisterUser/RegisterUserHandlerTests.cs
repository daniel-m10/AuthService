using AuthService.Api.Domain.Entities;
using AuthService.Api.Domain.Interfaces;
using AuthService.Api.Features.RegisterUser;
using FluentAssertions;
using NSubstitute;

namespace AuthService.Tests.Features.RegisterUser
{
    [TestFixture]
    internal class RegisterUserHandlerTests
    {
        private IUserRepository _userRepository = null!;
        private RegisterUserHandler _handler = null!;

        [SetUp]
        public void Setup()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new RegisterUserHandler(_userRepository);
        }

        [Test]
        public async Task HandleAsync_ShouldSucceed_WhenEmailIsNotTaken()
        {
            // Arrange
            var request = new RegisterUserRequest
            {
                Email = "newuser@mail.com",
                Password = "123456"
            };

            _userRepository.IsEmailTakenAsync(request.Email).Returns(false);

            // Act
            var result = await _handler.HandleAsync(request);

            // Assert
            result.ISuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            await _userRepository.Received(1).CreateUserAsync(Arg.Any<User>());
        }

        [Test]
        public async Task HandleAsync_ShouldFail_WhenEmailIsAlreadyTaken()
        {
            // Arrange
            var request = new RegisterUserRequest
            {
                Email = "existing@mail.com",
                Password = "123456"
            };

            _userRepository.IsEmailTakenAsync(request.Email).Returns(true);

            // Act
            var result = await _handler.HandleAsync(request);

            // Assert
            result.ISuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("Email is already taken.");
            result.Value.Should().BeNull();

            await _userRepository.DidNotReceive().CreateUserAsync(Arg.Any<User>());
        }
    }
}
