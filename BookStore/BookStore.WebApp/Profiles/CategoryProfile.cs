using System;
using AutoMapper;
using BookStore.WebApp.Models;
using BookStore.WebApp.ViewModels;

namespace BookStore.WebApp.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryViewModel>();
        }
    }
}