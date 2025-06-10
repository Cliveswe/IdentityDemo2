using IdentityDemo.Application.Dtos;
using IdentityDemo.Infrastructure.Persistence;
using IdentityDemo.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
namespace IdentityDemo.Infrastructure.Tests;

public class IdentityUserServiceTests
{
    [Fact]
    public async Task CreateUserAsync_With_User_Details() {
        // Arrange
        var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
        var mockUserManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<ApplicationUser>>().Object,
            new IUserValidator<ApplicationUser>[0],
            new IPasswordValidator<ApplicationUser>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<ApplicationUser>>>().Object);

        var mockContextAccessor = new Mock<IHttpContextAccessor>();
        var mockClaimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
        var mockSignInManager = new Mock<SignInManager<ApplicationUser>>(
            mockUserManager.Object,
            mockContextAccessor.Object,
            mockClaimsFactory.Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
            new Mock<IAuthenticationSchemeProvider>().Object,
            new Mock<IUserConfirmation<ApplicationUser>>().Object
        );

        var mockRoleStore = new Mock<IRoleStore<IdentityRole>>();
        var mockRoleManager = new Mock<RoleManager<IdentityRole>>(
            mockRoleStore.Object,
            new IRoleValidator<IdentityRole>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<ILogger<RoleManager<IdentityRole>>>().Object
        );

        var user = new UserProfileDto("test@user.com", "Test", "User");

        var service = new IdentityUserService(mockUserManager.Object, mockSignInManager.Object, mockRoleManager.Object);

        //Act
        var result = await service.CreateUserAsync(user, "Password_123");

        //Assert
        mockUserManager.Verify(mockUserManager =>
        mockUserManager.CreateAsync(
            It.Is<ApplicationUser>(u => u.Email == "test@user.com")), Times.Once);

        mockUserManager.Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
     .ReturnsAsync(IdentityResult.Success);

        Assert.True(result.Succeeded);
    }
}
