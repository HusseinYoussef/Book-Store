using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.WebApp.Models
{
    public class Gallery
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Path { get; set; }

        // Navigation Property
        public Book Book { get; set; }
    }
}