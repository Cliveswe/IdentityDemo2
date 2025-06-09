using IdentityDemo.Application.Dtos;

namespace IdentityDemo.Application.Users;

public interface IIdentityUserService
{
    Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password);
    Task<UserResultDto> SignInAsync(string email, string password);

    Task SignOutAsync();
}