using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IdentityDemo.Application.Users;
using IdentityDemo.Application.Dtos;
using IdentityDemo.Web.Views.Account;

namespace IdentityDemo.Web.Controllers;

public class AccountController(IUserService userService) : Controller
{
    [Authorize]
    [HttpGet("")]
    [HttpGet("members")]
    public IActionResult Members()
    {
        return View();
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterVM viewModel)
    {
        if (!ModelState.IsValid)
            return View();

        // Try to register user
        var userDto = new UserProfileDto(viewModel.Email, viewModel.FirstName, viewModel.LastName);
        var result = await userService.CreateUserAsync(userDto, viewModel.Password);
        if (!result.Succeeded)
        {
            // Show error
            ModelState.AddModelError(string.Empty, result.ErrorMessage!);
            return View();
        }

        // Redirect user
        return RedirectToAction(nameof(Login));
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginVM viewModel)
    {
        if (!ModelState.IsValid)
            return View();

        // Check if credentials is valid (and set auth cookie)
        var result = await userService.SignInAsync(viewModel.Username, viewModel.Password);
        if (!result.Succeeded)
        {
            // Show error
            ModelState.AddModelError(string.Empty, result.ErrorMessage!);
            return View();
        }

        // Redirect user
        return RedirectToAction(nameof(Members));
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await userService.SignOutAsync();

        return RedirectToAction(nameof(LoginAsync).Replace("Async", ""));
    }
}
