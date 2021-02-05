using System;
using System.Collections.Generic;
using BookStore.WebApp.Models;

namespace BookStore.WebApp.Data
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int id);
        Book AddBook(Book newBook);
    }
}