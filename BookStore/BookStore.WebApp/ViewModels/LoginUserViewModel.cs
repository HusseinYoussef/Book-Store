using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.WebApp.ViewModels
{
    public class LoginUserViewModel
    {
        [Required]
        [Display(Name="Email Address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name="Remember Me")]
        public bool RememberMe { get; set; }
    }
}