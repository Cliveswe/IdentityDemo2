using IdentityDemo.Application.Dtos;
using IdentityDemo.Application.Users;
using IdentityDemo.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace IdentityDemo.Infrastructure.Services;
public class IdentityUserService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    RoleManager<IdentityRole> roleManager) : IIdentityUserService
{
    public async Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password) {
        const string roleName = "Administrator";


        var result = await userManager.CreateAsync(new ApplicationUser {
            UserName = user.Email,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName
        }, password);

        if(user.Email.Contains("admin")) {

            if(!await roleManager.RoleExistsAsync(roleName)) {
                // Skapa en ny roll
                var resultRole = await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // Lägg till en användare till en roll
            //await userManager.AddToRoleAsync(resultRole, roleName);

        }
        return new UserResultDto(result.Errors.FirstOrDefault()?.Description);
    }

    public async Task<UserResultDto> SignInAsync(string email, string password) {
        var result = await signInManager.PasswordSignInAsync(email, password, false, false);
        return new UserResultDto(result.Succeeded ? null : "Invalid user credentials");
    }

    public async Task SignOutAsync() {
        await signInManager.SignOutAsync();
    }
}
