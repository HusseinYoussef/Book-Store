using System;
using System.Threading.Tasks;
using BookStore.WebApp.Data;
using BookStore.WebApp.Enums;
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

        [HttpGet("login")]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserViewModel user, string returnUrl)
        {
            if(!ModelState.IsValid)
            {
                return View(user);
            }
            var result = await _userRepository.SignInUser(user);
            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or Password is invalid");
                return View(user);
            }
            if(!string.IsNullOrEmpty(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("signup")]
        public ViewResult Signup()
        {
            return View();
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(CreateUserViewModel newUser)
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
            
            await _userRepository.SignInUser(new LoginUserViewModel(){
                Email=newUser.Email,
                Password=newUser.Password
            });
            return RedirectToAction("Index", "Home", new {userStatus=UserStatus.NewUser});
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userRepository.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}