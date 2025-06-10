using IdentityDemo.Application.Dtos;
using IdentityDemo.Infrastructure.Persistence;
using IdentityDemo.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Moq;
namespace IdentityDemo.Infrastructure.Tests;

public class IdentityUserServiceTests
{
    [Fact]
    public async Task CreateUserAsync_With_User_Details() {
        // Arrange
        var mockUserManager = new Mock<UserManager<ApplicationUser>>();
        var mockSignInManager = new Mock<SignInManager<ApplicationUser>>();
        var mockRoleManager = new Mock<RoleManager<IdentityRole>>();
        var user = new UserProfileDto {
            Email = "test@user.com",
            FirstName = "Test",
            LastName = "User"
        };

        var service = new IdentityUserService(mockUserManager.Object, mockSignInManager.Object, mockRoleManager.Object);

        //Act
        var result = await service.CreateUserAsync(user, "password123");

        //Assert
        mockUserManager.Verify(mockUserManager =>
        mockUserManager.CreateAsync(
            It.Is<ApplicationUser>(u => u.Email == "test@testing")), Times.Once);
        Assert.Null(result);
    }
}
