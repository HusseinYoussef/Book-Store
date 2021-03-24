using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.WebApp.Models;
using BookStore.WebApp.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace BookStore.WebApp.Data
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUser(CreateUserViewModel newUser);
        Task<SignInResult> SignInUser(LoginUserViewModel user);
        Task SignOut();
        Task<IdentityResult> AssignUserRoles(string userEmail, IEnumerable<string> roles);
        Task<IEnumerable<string>> GetUserRoles(string userId);
        Task<IdentityResult> ChangePassword(ChangePasswordViewModel model);
    }
}