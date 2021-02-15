using System;
using System.Threading.Tasks;
using BookStore.WebApp.Models;
using BookStore.WebApp.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace BookStore.WebApp.Data
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public SqlUserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUser(UserViewModel newUser)
        {
            var user = new User(){
                UserName=newUser.Email,
                Email=newUser.Email,
                FirstName=newUser.FirstName,
                LastName=newUser.LastName,
                DateOfBirth=newUser.DateOfBirth
            };

            var result = await _userManager.CreateAsync(user, newUser.Password);
            return result;
        }
    }
}