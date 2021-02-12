using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookStore.WebApp.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.WebApp.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength=3)]
        public string Title { get; set; }

        [Required]
        [StringLength(50, MinimumLength=5)]
        public string Author { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage="Please select a language")]
        [Display(Name="Language")]
        public int LanguageId { get; set; }

        public string LanguageName { get; set; }
        
        [Required]
        [Range(100, 1000, ErrorMessage="Pages number should be in range of 100 and 1000")]
        [Display(Name="Number of Pages")]
        public int? TotalPages { get; set; }

        [Required(ErrorMessage="Please select one or more categories")]
        [Display(Name="Category")]
        public IEnumerable<int> CategoryIds { get; set; }

        public IEnumerable<string> CategoryNames { get; set; }

        [Required]
        [ImageExtensions]
        [Display(Name="Choose Cover Photo")]
        public IFormFile CoverPhoto { get; set; }

        public string CoverPhotoPath { get; set; }
    }
}