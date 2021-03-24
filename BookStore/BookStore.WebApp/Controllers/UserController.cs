using System;
using System.Collections.Generic;
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
            result = await _userRepository.AssignUserRoles(newUser.Email, new List<string>() { "Normal User" });
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

        [HttpGet("change-password")]
        public ViewResult ChangePassword()
        {
            return View();
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(changePasswordViewModel);
            }
            var result = await _userRepository.ChangePassword(changePasswordViewModel);
            if(!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(changePasswordViewModel);
            }
            ModelState.Clear();
            ViewBag.Success = true;
            return View();
        }
    }
}