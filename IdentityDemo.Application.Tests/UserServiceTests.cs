using IdentityDemo.Application.Dtos;
using IdentityDemo.Application.Users;
using Moq;

namespace IdentityDemo.Application.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task CreateUserAsync_WithDtoAndPassword()
        {
            // Arrange
            var mockIdentityUserService = new Mock<IIdentityUserService>();
            var userService = new UserService(mockIdentityUserService.Object);
            var user = new UserProfileDto("John@mail.com", "John", "Doe");
            var userResult = new UserResultDto(null);
            mockIdentityUserService.Setup(s => s.CreateUserAsync(user, "Password_123")).ReturnsAsync(userResult);

            // Act
            var result = await userService.CreateUserAsync(user, "Password_123");

            // Assert
            Assert.Equal(userResult, result);
            mockIdentityUserService.Verify(s => s.CreateUserAsync(user, "Password_123"), Times.Once);
        }

        [Fact]
        public async Task SignInAsync_ValidEmailAndPassword()
        {
            // Arrange
            var mockIdentityUserService = new Mock<IIdentityUserService>();
            var userService = new UserService(mockIdentityUserService.Object);
            var userResult = new UserResultDto(null);
            mockIdentityUserService.Setup(s =>
                s.SignInAsync("test@mail.com", "Password_123")).ReturnsAsync(userResult);

            // Act

            var result = await userService.SignInAsync("test@mail.com", "Password_123");

            // Assert
            Assert.Equal(userResult, result);
            mockIdentityUserService.Verify(s => s.SignInAsync("test@mail.com", "Password_123"), Times.Once);
        }

        [Fact]
        public async Task SignOutAsync_Test()
        {
            // Arrange
            var mockIdentityUserService = new Mock<IIdentityUserService>();
            var userService = new UserService(mockIdentityUserService.Object);

            // Act
            await userService.SignOutAsync();

            // Assert

            mockIdentityUserService.Verify(s => s.SignOutAsync(), Times.Once);
        }
    }
}
