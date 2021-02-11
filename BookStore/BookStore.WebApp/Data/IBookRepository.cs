using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.WebApp.Models;

namespace BookStore.WebApp.Data
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBookById(int id);
        Task<int> AddBook(Book newBook);
    }
}