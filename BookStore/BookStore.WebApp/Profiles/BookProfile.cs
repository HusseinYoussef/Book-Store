using System;
using System.Linq;
using AutoMapper;
using BookStore.WebApp.Models;
using BookStore.WebApp.ViewModels;

namespace BookStore.WebApp.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.CategoryNames, opt =>
                                    opt.MapFrom(src => src.Category.Select(c => c.Name)))
                                            .ForMember(dest => dest.CategoryIds, opt =>
                                    opt.MapFrom(src => src.Category.Select(c => c.Id)));
            CreateMap<BookViewModel, Book>();
        }
    }
}