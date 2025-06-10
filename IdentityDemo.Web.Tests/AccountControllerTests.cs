using IdentityDemo.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using IdentityDemo.Application.Users;
using IdentityDemo.Application.Dtos;
using IdentityDemo.Web.Views.Account;

namespace IdentityDemo.Web.Tests;

public class AccountControllerTests
{
    [Fact]
    public async Task Register_WithRegisterVM_ReturnsRedirectToActionResult()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        var accountController = new AccountController(mockUserService.Object);
        var registerVM = new RegisterVM()
        { Email = "John@mail.com", FirstName = "John", LastName = "Doe", Password = "Password_123" };
        var userDto = new UserProfileDto(registerVM.Email, registerVM.FirstName, registerVM.LastName);
        var userResult = new UserResultDto(null);
        mockUserService.Setup(s => s.CreateUserAsync(userDto, registerVM.Password))
            .ReturnsAsync(userResult);

        // Act
        var result = await accountController.RegisterAsync(registerVM);

        // Assert
        var viewResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Login", viewResult.ActionName);
        mockUserService.Verify(s => s.CreateUserAsync(userDto, registerVM.Password), Times.Once);
    }

    [Fact]
    public async Task Login_WithValidInput_ReturnsRedirectToActionResult()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        var accountController = new AccountController(mockUserService.Object);
        var loginVM = new LoginVM() { Username = "John@mail.com", Password = "Password_123" };
        var userResult = new UserResultDto(null);
        mockUserService.Setup(s => s.SignInAsync(loginVM.Username, loginVM.Password))
            .ReturnsAsync(userResult);

        // Act
        var result = await accountController.LoginAsync(loginVM);

        // Assert
        var viewResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Members", viewResult.ActionName);
        mockUserService.Verify(s => s.SignInAsync(loginVM.Username, loginVM.Password), Times.Once);
    }
}
