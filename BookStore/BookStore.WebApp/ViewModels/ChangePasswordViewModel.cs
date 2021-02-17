using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.WebApp.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Current Password")]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name="New Password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage="Confirm password doesn't match with the new password")]
        [Display(Name="Confirm Password")]
        public string ConfirmPassword { get; set; }    
    }
}