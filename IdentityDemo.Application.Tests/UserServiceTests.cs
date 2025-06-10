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


    }
}
