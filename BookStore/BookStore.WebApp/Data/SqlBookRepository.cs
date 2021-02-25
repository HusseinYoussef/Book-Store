using System;
using System.Linq;
using System.Collections.Generic;
using BookStore.WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AutoMapper;

namespace BookStore.WebApp.Data
{
    public class SqlBookRepository : IBookRepository
    {
        private readonly BookStoreDbContext _context;

        public SqlBookRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddBook(Book newBook)
        {
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;
        }

        public async Task<bool> CheckBookName(string name)
        {
            Book book = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Title==name);
            return book!=null;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            IEnumerable<Book> books = await _context.Books.AsNoTracking().ToListAsync();
            return books;
        }

        public async Task<Book> GetBookById(int id)
        {
            Book book = await _context.Books.FindAsync(id);
            await _context.Entry(book)
                          .Collection(b => b.Category)
                          .LoadAsync();
            await _context.Entry(book)
                          .Collection(b => b.BookGallery)
                          .LoadAsync();
            await _context.Entry(book)
                          .Reference(b => b.Language)
                          .LoadAsync();
            return book;
        }

        public async Task<IEnumerable<Book>> GetSimilarBooks(int bookId, IEnumerable<string> categories, int count)
        {
            IEnumerable<Book> books = await _context.Books.AsNoTracking()
                                                    .Where(b => (b.Id != bookId) && (b.Category.Select(c => c.Name).Any(i => categories.Contains(i))))
                                                    .OrderBy(b => b.Id)
                                                    .Take(count)
                                                    .ToListAsync();
            return books;
        }

        public async Task<IEnumerable<Book>> GetTopBooks(int count=3)
        {
            IEnumerable<Book> books = await _context.Books.AsNoTracking()
                                                    .OrderBy(b => b.Id).Take(count).ToListAsync();
            return books;
        }

        public async Task<IEnumerable<Book>> Search(string bookName)
        {
            IEnumerable<Book> books = await _context.Books.AsNoTracking()
                                                    .Where(b => b.Title.ToLower().Contains(bookName.ToLower()))
                                                    .ToListAsync();
            return books;
        }
    }
}