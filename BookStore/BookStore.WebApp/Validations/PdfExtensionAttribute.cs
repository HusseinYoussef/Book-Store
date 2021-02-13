using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace BookStore.WebApp.Validations
{
    public class PdfExtensionAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public PdfExtensionAttribute()
        {
            _extensions = new string[] {".pdf"};
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if(file != null)
            {
                if(_extensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(ErrorMessage ?? "Pdf file extension not valid");
        }
    }
}