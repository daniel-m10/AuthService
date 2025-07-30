using AuthService.Api.Domain.Entities;
using AuthService.Api.Infrastructure;

namespace AuthService.Tests.Infrastructure
{
    [TestFixture]
    internal class InMemoryUserRepositoryTests
    {
        [Test]
        public async Task IsEmailTakenAsync_ShouldReturnFalse_WhenEmailNotExists()
        {
            // Arrange
            var repository = GetRepository();

            // Act
            var result = await repository.IsEmailTakenAsync("test@mail.com");

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task IsEmailTakenAsync_ShouldReturnTrue_WhenEmailAlreadyExists()
        {
            // Arrange
            var repository = GetRepository();

            var user = new User
            {
                Email = "existing@mail.com",
                PasswordHash = "password123",
            };

            // Act
            await repository.CreateUserAsync(user);
            var result = await repository.IsEmailTakenAsync("existing@mail.com");

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task CreateUserAsync_ShouldAddUserToMemory()
        {
            // Arrange
            var repository = GetRepository();

            var user = new User
            {
                Email = "new@mail.com",
                PasswordHash = "password123",
            };

            // Act
            await repository.CreateUserAsync(user);
            var users = repository.GetAllUsers();

            // Assert
            Assert.That(users.Count, Is.EqualTo(1));
            Assert.That(users.First().Email, Is.EqualTo("new@mail.com"));
        }

        [Test]
        public async Task GetUserByEmailAsync_ShouldReturnUser_WhenEmailExists()
        {
            // Arrange
            var repository = GetRepository();

            var user = new User
            {
                Email = "new@mail.com",
                PasswordHash = "password123",
            };

            await repository.CreateUserAsync(user);

            // Act
            var result = await repository.GetUserByEmailAsync("new@mail.com");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Email, Is.EqualTo("new@mail.com"));
                Assert.That(result.PasswordHash, Is.EqualTo("password123"));
            });
        }

        [Test]
        public async Task GetUserByEmailAsync_ShouldReturnNull_WhenEmailDoesNotExist()
        {
            // Arrange
            var repository = GetRepository();

            // Act
            var result = await repository.GetUserByEmailAsync("notexisting@mail.com");

            // Assert
            Assert.That(result, Is.Null);
        }

        private static InMemoryUserRepository GetRepository()
        {
            return new InMemoryUserRepository();
        }
    }
}
