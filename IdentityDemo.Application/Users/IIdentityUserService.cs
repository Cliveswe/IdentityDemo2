using IdentityDemo.Application.Dtos;

namespace IdentityDemo.Application.Users;

public interface IIdentityUserService
{
    Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password);
    Task<UserProfileDto?> GetUserByIdAsync(string userId);
    Task<UserResultDto> SignInAsync(string email, string password);

    Task SignOutAsync();
}