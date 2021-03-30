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
using BookStore.WebApp.Services;

namespace BookStore.WebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IUserService _userService;

        public BookController(
                            IBookRepository bookRepository,
                            ILanguageRepository languageRepository,
                            ICategoryRepository categoryRepository,
                            IMapper mapper,
                            IWebHostEnvironment env,
                            IUserService userService
                        )
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _env = env;
            _userService = userService;
        }

        [Route("Books/AllBooks")]
        public async Task<ViewResult> ShowBooks()
        {
            IEnumerable<Book> books = await _bookRepository.GetAllBooks();
            return View(_mapper.Map<IEnumerable<BookViewModel>>(books));
        }

        [Route("Books/BookDetails")]
        public async Task<ViewResult> GetBookDetails(int id, BookStatus bookStatus = BookStatus.Default)
        {
            Book book = await _bookRepository.GetBookById(id);
            BookViewModel bookModel = _mapper.Map<BookViewModel>(book);
            bookModel.Gallery = book.BookGallery.Select(bg => _mapper.Map<GalleryViewModel>(bg)).ToList();
            ViewBag.status = bookStatus;
            return View(bookModel);
        }

        [Authorize]
        [HttpGet("Books/AddBook")]
        public ViewResult AddBook(BookStatus status = BookStatus.Default, int bookId = 0)
        {
            ViewBag.bookId = bookId;
            ViewBag.status = status;
            return View();
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost("Books/AddBook")]
        public async Task<IActionResult> AddBook(BookViewModel newBook)
        {
            if (!ModelState.IsValid)
            {
                return View(newBook);
            }
            if (await _bookRepository.CheckBookName(newBook.Title))
            {
                ModelState.AddModelError("", "Book Title is already used");
                return View(newBook);
            }

            Book book = _mapper.Map<Book>(newBook);
            // Upload Cover Photo
            book.CoverPhotoPath = await UploadFile("books/images/cover/", newBook.CoverPhoto);
            // Upload Pdf
            book.PdfPath = await UploadFile("books/pdf/", newBook.Pdf);
            // Upload gallery Photos
            book.BookGallery = new List<Gallery>();
            foreach (var file in newBook.GalleryFiles)
            {
                book.BookGallery.Add(new Gallery()
                {
                    Name = file.FileName,
                    Path = await UploadFile("books/images/gallery/", file)
                });
            }
            IEnumerable<Category> categories = await _categoryRepository.GetCategoriesById(newBook.CategoryIds);
            book.Category = new List<Category>();
            foreach (Category category in categories)
            {
                book.Category.Add(category);
            }
            book.CreatedAt = book.UpdatedAt = DateTime.UtcNow;
            book.UserId = _userService.GetUserId();
            int bookId = await _bookRepository.AddBook(book);
            return RedirectToAction(nameof(AddBook), new { status = BookStatus.Success, bookId = bookId });
        }

        private async Task<string> UploadFile(string folder, IFormFile file)
        {
            string path = folder + Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine(_env.WebRootPath, path);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + path;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string by, [FromQuery(Name = "search_query")] string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.by = by;
            ViewBag.searchQuery = searchQuery;
            IEnumerable<Book> books = null;
            if (by == "title")
            {
                books = await _bookRepository.SearchByTitle(searchQuery);
            }
            else if (by == "category")
            {
                books = await _bookRepository.SearchByCategory(searchQuery);
            }
            else if (by == "author")
            {
                books = await _bookRepository.SearchByAuthor(searchQuery);
            }

            return View("SearchResults", _mapper.Map<IEnumerable<BookViewModel>>(books));
        }

        [Authorize]
        [HttpGet("Library")]
        public async Task<ViewResult> Library()
        {
            IEnumerable<Book> books = await _bookRepository.GetLibrary(_userService.GetUserId());
            return View(_mapper.Map<IEnumerable<BookViewModel>>(books));
        }

        [Authorize]
        [HttpGet("Books/Edit")]
        public async Task<IActionResult> EditBook(int id)
        {
            Book book = await _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<BookViewModel>(book));
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost("Books/Edit")]
        public async Task<IActionResult> EditBook(BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookViewModel);
            }
            if (await _bookRepository.CheckBookName(bookViewModel.Title, bookViewModel.Id))
            {
                ModelState.AddModelError("", "Book Title is already used");
                return View(bookViewModel);
            }

            Book book = await _bookRepository.GetBookById(bookViewModel.Id);
            _mapper.Map(bookViewModel, book);
            book.CoverPhotoPath = await UploadFile("books/images/cover/", bookViewModel.CoverPhoto);
            book.PdfPath = await UploadFile("books/pdf/", bookViewModel.Pdf);
            book.BookGallery = new List<Gallery>();
            foreach (var file in bookViewModel.GalleryFiles)
            {
                book.BookGallery.Add(new Gallery()
                {
                    Name = file.FileName,
                    Path = await UploadFile("books/images/gallery/", file)
                });
            }
            IEnumerable<Category> categories = await _categoryRepository.GetCategoriesById(bookViewModel.CategoryIds);
            book.Category = new List<Category>();
            foreach (Category category in categories)
            {
                book.Category.Add(category);
            }
            book.UpdatedAt = DateTime.UtcNow;
            await _bookRepository.SaveUpdates();
            return RedirectToAction(nameof(GetBookDetails), new {id=book.Id, bookStatus=BookStatus.Success});
        }

        [Authorize]
        [HttpDelete("Books/Delete")]
        public async Task<IActionResult> DeleteBook([FromQuery] int id)
        {
            Book book = await _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            await _bookRepository.DeleteBook(book);
            return Ok();
        }
    }
}