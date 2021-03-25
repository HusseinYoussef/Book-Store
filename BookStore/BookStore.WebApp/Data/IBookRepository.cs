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
        Task<IEnumerable<Book>> GetSimilarBooks(int bookId, IEnumerable<string> categories, int count);
        Task<IEnumerable<Book>> GetLibrary(string userId);
        Task<Book> GetBookById(int id);
        Task<bool> CheckBookName(string name);
        Task<int> AddBook(Book newBook);
        Task<IEnumerable<Book>> SearchByTitle(string bookTitle);
        Task<IEnumerable<Book>> SearchByCategory(string bookCategory);
        Task<IEnumerable<Book>> SearchByAuthor(string bookAuthor);
    }
}