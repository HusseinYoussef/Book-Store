using System;
using System.Threading.Tasks;
using BookStore.WebApp.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace BookStore.WebApp.Data
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUser(CreateUserViewModel newUser);
        Task<SignInResult> SignInUser(LoginUserViewModel user);
        Task SignOut();

        Task<IdentityResult> ChangePassword(ChangePasswordViewModel model);
    }
}