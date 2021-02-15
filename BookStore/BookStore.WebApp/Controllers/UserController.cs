using System;
using System.Threading.Tasks;
using BookStore.WebApp.Data;
using BookStore.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpGet("signup")]
        public ViewResult Signup()
        {
            return View();
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(UserViewModel newUser)
        {
            if(!ModelState.IsValid)
            {
                return View(newUser);
            }

            var result = await _userRepository.CreateUser(newUser);
            if(!result.Succeeded)
            {
                foreach(var errorMessage in result.Errors)
                {
                    ModelState.AddModelError("", errorMessage.Description);
                }
                return View(newUser);
            }
            ModelState.Clear();
            return RedirectToAction(nameof(Signup));
        }
    }
}