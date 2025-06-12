// Ignore Spelling: Dto

using IdentityDemo.Application.Dtos;
using IdentityDemo.Application.Users;
using Moq;

namespace IdentityDemo.Application.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task CreateUserAsync_WithDtoAndPassword() {
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
        public async Task SignInAsync_ValidEmailAndPassword() {
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
        public async Task SignOutAsync_Test() {
            // Arrange
            var mockIdentityUserService = new Mock<IIdentityUserService>();
            var userService = new UserService(mockIdentityUserService.Object);

            // Act
            await userService.SignOutAsync();

            // Assert

            mockIdentityUserService.Verify(s => s.SignOutAsync(), Times.Once);
        }

        [Fact]
        public async Task GetUserByIdAsync_ValidUserId_ShouldReturnUserProfileDto() {

            // Arrange
            var userId = "12345";
            var expectedUser = new UserProfileDto("test@email.com", "John", "Doe");
            var identityUserServiceMock = new Mock<IIdentityUserService>();
            var userService = new UserService(identityUserServiceMock.Object);

            identityUserServiceMock.Setup(service => service.GetUserByIdAsync(userId))
                .ReturnsAsync(expectedUser);
            // Act
            var result = await userService.GetUserByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserProfileDto>(result);
            Assert.Equal(expectedUser.Email, result.Email);
            Assert.Equal(expectedUser.FirstName, result.FirstName);
            Assert.Equal(expectedUser.LastName, result.LastName);
            identityUserServiceMock.Verify(service => service.GetUserByIdAsync(userId), Times.Once);
        }
    }
}

