using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace BookStore.WebApp.Validations
{
    public class ImageExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public ImageExtensionsAttribute()
        {
            _extensions = new string[] {".jpg", ".png"};
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if(file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if(_extensions.Contains(extension.ToLower()))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("Image Extension should be jpg or png");
        }
    }
}