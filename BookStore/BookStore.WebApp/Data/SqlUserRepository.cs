using System;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.WebApp.Models;
using BookStore.WebApp.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace BookStore.WebApp.Data
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public SqlUserRepository(UserManager<User> userManager, SignInManager<User> signInManager,
                                IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> CreateUser(CreateUserViewModel newUser)
        {
            var user = _mapper.Map<User>(newUser);
            user.UserName = newUser.Email;

            var result = await _userManager.CreateAsync(user, newUser.Password);
            return result;
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