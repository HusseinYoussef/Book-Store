using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookStore.WebApp.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

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
        [Newtonsoft.Json.JsonIgnore]
        public int LanguageId { get; set; }

        [JsonProperty("Language")]
        public string LanguageName { get; set; }
        
        [Required]
        [Range(100, 1000, ErrorMessage="Pages number should be in range of 100 and 1000")]
        [Display(Name="Number of Pages")]
        [JsonProperty("Total Pages")]
        public int? TotalPages { get; set; }

        [Required(ErrorMessage="Please select one or more categories")]
        [Display(Name="Category")]
        [Newtonsoft.Json.JsonIgnore]
        public IEnumerable<int> CategoryIds { get; set; }

        [JsonProperty("Categories")]
        public IEnumerable<string> CategoryNames { get; set; }

        [Required]
        [ImageExtensions]
        [Display(Name="Choose Cover Photo")]
        [Newtonsoft.Json.JsonIgnore]
        public IFormFile CoverPhoto { get; set; }

        [JsonProperty("Cover Photo")]
        public string CoverPhotoPath { get; set; }

        [Required]
        [ImageExtensions]
        [Display(Name="Gallery")]
        [Newtonsoft.Json.JsonIgnore]
        public IFormFileCollection GalleryFiles { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public List<GalleryViewModel> Gallery { get; set; }

        [Required]
        [Display(Name="Upload book in pdf format")]
        [PdfExtension]
        [Newtonsoft.Json.JsonIgnore]
        public IFormFile Pdf { get; set; }

        [JsonProperty("Pdf")]
        public string PdfPath { get; set; }

        public string UserId { get; set; }
    }
}