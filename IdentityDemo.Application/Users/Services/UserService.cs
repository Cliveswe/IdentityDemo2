using IdentityDemo.Application.Dtos;
using IdentityDemo.Application.Users.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityDemo.Application.Users.Services;
public class UserService(IIdentityUserService identityUserService) : IUserService
{
    public async Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password) =>
        await identityUserService.CreateUserAsync(user, password);

    public async Task<UserResultDto> SignInAsync(string email, string password) =>
        await identityUserService.SignInAsync(email, password);

    public async Task SignOutAsync() => await identityUserService.SignOutAsync();
}