using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.WebApp.Models
{
    public enum BookCategory
    {
        Fantasy,
        History,
        Romance,
        Thriller,
        Adventure,
        [Display(Name="Science Fiction")]
        ScienceFiction,
        Mystery,
        Development,
        [Display(Name="Guide / How To")]
        Guide,
        Art,
        [Display(Name="Children's")]
        Childrens
    }
}