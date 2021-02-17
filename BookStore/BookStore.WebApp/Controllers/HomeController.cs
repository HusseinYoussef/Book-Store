using System;
using BookStore.WebApp.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index(UserStatus userStatus=UserStatus.NormalUser)
        {
            ViewBag.userStatus = userStatus;
            return View();
        }
    }
}