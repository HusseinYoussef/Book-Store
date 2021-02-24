using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.WebApp.Models;

namespace BookStore.WebApp.Data
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<IEnumerable<Book>> GetTopBooks(int count);
        Task<Book> GetBookById(int id);
        Task<bool> CheckBookName(string name);
        Task<int> AddBook(Book newBook);
        Task<IEnumerable<Book>> Search(string bookName);
    }
}