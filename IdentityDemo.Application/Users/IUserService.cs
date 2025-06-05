using IdentityDemo.Application.Dtos;

namespace IdentityDemo.Application.Users;
public interface IUserService
{
    Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password);
    Task<UserResultDto> SignInAsync(string email, string password);
}