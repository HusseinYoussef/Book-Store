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
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

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
            BookViewModel bookModel = _mapper.Map<BookViewModel>(book);
            bookModel.Gallery = book.BookGallery.Select(bg => _mapper.Map<GalleryViewModel>(bg)).ToList();
            return View(bookModel);
        }

        [Authorize]
        [HttpGet("Books/AddBook")]
        public ViewResult AddBook(AddBookStatus status=AddBookStatus.Default, int bookId=0)
        {
            ViewBag.bookId = bookId;
            ViewBag.status = status;
            return View();
        }

        [HttpPost("Books/AddBook")]
        public async Task<IActionResult> AddBook(BookViewModel newBook)
        {
            if(!ModelState.IsValid)
            {
                return View(newBook);
            }
            if(await _bookRepository.CheckBookName(newBook.Title))
            {
                ViewBag.status = AddBookStatus.Fail;
                return View(newBook);
            }

            Book book = _mapper.Map<Book>(newBook);
            // Upload Cover Photo
            book.CoverPhotoPath = await UploadImage("books/images/cover/", newBook.CoverPhoto);
            // Upload Pdf
            book.PdfPath = await UploadImage("books/pdf/", newBook.Pdf);
            // Upload gallery Photos
            book.BookGallery = new List<Gallery>();
            foreach(var file in newBook.GalleryFiles)
            {
                book.BookGallery.Add(new Gallery(){
                    Name=file.FileName,
                    Path=await UploadImage("books/images/gallery/", file)
                });
            }
            IEnumerable<Category> categories = await _categoryRepository.GetCategoriesById(newBook.CategoryIds);
            book.Category = new List<Category>();
            foreach(Category category in categories)
            {
                book.Category.Add(category);
            }
            book.CreatedAt = book.UpdatedAt = DateTime.UtcNow;
            int bookId = await _bookRepository.AddBook(book);
            return RedirectToAction(nameof(AddBook), new {status=AddBookStatus.Success, bookId=bookId});
        }

        private async Task<string> UploadImage(string folder, IFormFile file)
        {
            string path = folder + Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine(_env.WebRootPath, path);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            
            return "/" + path;
        }

        [HttpGet("results")]
        public async Task<IActionResult> SearchResults([FromQuery] string searchQuery)
        {
            IEnumerable<Book> books = await _bookRepository.Search(searchQuery);
            return View(_mapper.Map<IEnumerable<BookViewModel>>(books));
        }

        [HttpPost("results")]
        public IActionResult Search(string searchQuery)
        {
            if(string.IsNullOrEmpty(searchQuery))
            {
                return RedirectToAction("Index", "Home");
            }
            var parametersToAdd = new Dictionary<string, string> { { "searchQuery", searchQuery } };
            var newUri = Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString("", parametersToAdd);
            return Redirect(newUri);
        }
    }
}