using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.WebApp.Data;
using BookStore.WebApp.ViewModels;
using BookStore.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using BookStore.WebApp.Enums;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace BookStore.WebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public BookController(
                            IBookRepository bookRepository,
                            ILanguageRepository languageRepository,
                            ICategoryRepository categoryRepository,
                            IMapper mapper,
                            IWebHostEnvironment env
                        )
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _env = env;
        }

        [Route("Books/AllBooks")]
        public async Task<ViewResult> ShowBooks()
        {
            IEnumerable<Book> books = await _bookRepository.GetAllBooks();
            return View(_mapper.Map<IEnumerable<BookViewModel>>(books));
        }

        [Route("Books/BookDetails")]
        public async Task<ViewResult> GetBookDetails(int id)
        {
            Book book = await _bookRepository.GetBookById(id);
            return View(_mapper.Map<BookViewModel>(book));
        }

        [HttpGet("Books/AddBook")]
        public async Task<ViewResult> AddBook(AddBookStatus status=AddBookStatus.Default, int bookId=0)
        {
            ViewBag.Languages = _mapper.Map<List<LanguageViewModel>>(await _languageRepository.GetAllLanguages());
            ViewBag.Categories = _mapper.Map<List<CategoryViewModel>>(await _categoryRepository.GetAllCategories());
            ViewBag.bookId = bookId;
            ViewBag.status = status;
            return View();
        }

        [HttpPost("Books/AddBook")]
        public async Task<IActionResult> AddBook(BookViewModel newBook)
        {
            ViewBag.Languages = _mapper.Map<List<LanguageViewModel>>(await _languageRepository.GetAllLanguages());
            ViewBag.Categories = _mapper.Map<List<CategoryViewModel>>(await _categoryRepository.GetAllCategories());
            if(!ModelState.IsValid)
            {
                return View(newBook);
            }
            string folder = "images/book/cover/";
            string path = folder + Guid.NewGuid().ToString() + "_" + newBook.CoverPhoto.FileName;
            string serverFolder = Path.Combine(_env.WebRootPath, path);
            await newBook.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            newBook.CoverPhotoPath = "/" + path;
            Book book = _mapper.Map<Book>(newBook);
            try
            {
                IEnumerable<Category> categories = await _categoryRepository.GetCategoriesById(newBook.CategoryIds);
                book.Category = new List<Category>();
                foreach(Category category in categories)
                {
                    book.Category.Add(category);
                }
                book.CreatedAt = book.UpdatedAt = DateTime.Now;
                int bookId = await _bookRepository.AddBook(book);
                return RedirectToAction(nameof(AddBook), new {status=AddBookStatus.Success, bookId=bookId});
            }
            catch
            {
                ViewBag.status = AddBookStatus.Fail;
                return View(newBook);
            }
        }
    }
}