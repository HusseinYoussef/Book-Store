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
        private readonly IMapper _mapper;

        public SqlUserRepository(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> CreateUser(UserViewModel newUser)
        {
            var user = _mapper.Map<User>(newUser);
            user.UserName = newUser.Email;
            user.Email = newUser.Email;

            var result = await _userManager.CreateAsync(user, newUser.Password);
            return result;
        }
    }
}