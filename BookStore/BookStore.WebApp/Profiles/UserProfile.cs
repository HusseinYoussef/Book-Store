using System;
using AutoMapper;
using BookStore.WebApp.Models;
using BookStore.WebApp.ViewModels;

namespace BookStore.WebApp.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserViewModel, User>();
        }
    }
}