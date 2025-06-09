// Ignore Spelling: Admin

using IdentityDemo.Application.Dtos;
using IdentityDemo.Application.Users.Interfaces;
using IdentityDemo.Web.Views.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityDemo.Web.Controllers;

public class AccountController(IUserService userService) : Controller
{
    [Authorize(Roles = "Administrator")]
    [HttpGet("admin")]
    public IActionResult Admin() {

        return View();
    }


    [Authorize]
    [HttpGet("")]
    [HttpGet("members")]
    public IActionResult Members() {
        return View();
    }

    [HttpGet("register")]
    public IActionResult Register() {
        return View();
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterVM viewModel) {
        if(!ModelState.IsValid)
            return View();

        // Try to register user
        var userDto = new UserProfileDto(viewModel.Email, viewModel.FirstName, viewModel.LastName);
        var result = await userService.CreateUserAsync(userDto, viewModel.Password);
        if(!result.Succeeded) {
            // Show error
            ModelState.AddModelError(string.Empty, result.ErrorMessage!);
            return View();
        }

        // Redirect user
        return RedirectToAction(nameof(Login));
    }

    [HttpGet("login")]
    public IActionResult Login() {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginVM viewModel) {
        if(!ModelState.IsValid)
            return View();

        // Check if credentials is valid (and set auth cookie)
        var result = await userService.SignInAsync(viewModel.Username, viewModel.Password);
        if(!result.Succeeded) {
            // Show error
            ModelState.AddModelError(string.Empty, result.ErrorMessage!);
            return View();
        }

        if(User.IsInRole("Administrator")) {
            // Redirect to admin page
            return RedirectToAction(nameof(Admin));
        }

        // Redirect user
        return RedirectToAction(nameof(Members));
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout() {
        await userService.SignOutAsync();

        return RedirectToAction(nameof(LoginAsync).Replace("Async", ""));
    }
}
