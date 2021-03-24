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

        [Required]
        public int? TotalPages { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [Required]
        public int LanguageId { get; set; }

        public string CoverPhotoPath { get; set; }

        public string PdfPath { get; set; }

        public string UserId { get; set; }

        // Navigation Properties
        public User User { get; set; }

        public Language Language { get; set; }
        
        public ICollection<Category> Category { get; set; }

        public ICollection<Gallery> BookGallery { get; set; }
    }
}