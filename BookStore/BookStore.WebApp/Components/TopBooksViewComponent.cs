using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.WebApp.Data;
using BookStore.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApp.Components
{
    public class TopBooksViewComponent : ViewComponent
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public TopBooksViewComponent(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var books = await _bookRepository.GetTopBooks(count);
            return View(_mapper.Map<IEnumerable<BookViewModel>>(books));
        }
    }
}