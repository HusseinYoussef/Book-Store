using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.WebApp.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Author { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage="Select one or more category")]
        [Display(Name="Categories")]
        public IEnumerable<string> Categories { get; set; }
    
        [Required(ErrorMessage="Select a language")]
        public BookLanguage? Language { get; set; }

        [Required]
        [Range(100, 1000, ErrorMessage="Pages number should be in range 100 and 1000")]
        [Display(Name="Number of Pages")]
        public int? TotalPages {get; set;}
    }
}