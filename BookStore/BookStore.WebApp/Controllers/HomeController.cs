using System;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}