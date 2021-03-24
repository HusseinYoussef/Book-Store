using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.WebApp.Models;
using BookStore.WebApp.Services;
using BookStore.WebApp.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace BookStore.WebApp.Data
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public SqlUserRepository(UserManager<User> userManager, SignInManager<User> signInManager,
                                IUserService userService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordViewModel model)
        {
            var user = await _userManager.FindByIdAsync(_userService.GetUserId());
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            return result;
        }

        public async Task<IdentityResult> CreateUser(CreateUserViewModel newUser)
        {
            var user = _mapper.Map<User>(newUser);
            user.UserName = newUser.FirstName + newUser.LastName;

            var result = await _userManager.CreateAsync(user, newUser.Password);
            return result;
        }

        public async Task<IdentityResult> AssignUserRoles(string userEmail, IEnumerable<string> roles)
        {
            User user = await _userManager.FindByEmailAsync(userEmail);
            var result = await _userManager.AddToRolesAsync(user, roles);

            return result;
        }

        public async Task<IEnumerable<string>> GetUserRoles(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            return roles;
        }

        public async Task<SignInResult> SignInUser(LoginUserViewModel user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);
            return result;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}