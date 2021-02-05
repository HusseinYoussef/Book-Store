using System;
using System.Collections.Generic;
using BookStore.WebApp.Data;
using BookStore.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public ViewResult ShowBooks()
        {
            IEnumerable<Book> books = _bookRepository.GetAllBooks();
            return View(books);
        }

        public ViewResult GetBook(int id)
        {
            Book book = _bookRepository.GetBookById(id);
            return View(book);
        }
    }
}