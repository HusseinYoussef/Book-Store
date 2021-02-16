using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.WebApp.ViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name="Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name="Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [Display(Name="Email Address")]
        [EmailAddress(ErrorMessage="Enter a Valid Email")]
        public string Email { get; set; }

        [Required]
        [Compare("ConfirmPassword", ErrorMessage="Password doesn't match")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name="Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}