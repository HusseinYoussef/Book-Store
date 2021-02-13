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
            Book book = await _context.Books.FirstOrDefaultAsync(b => b.Title==name);
            return book!=null;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            IEnumerable<Book> books = await _context.Books.ToListAsync();
            return books;
        }

        public async Task<Book> GetBookById(int id)
        {
            Book book = await _context.Books.Where(b => b.Id==id)
                                            .Include(b => b.Language)
                                            .Include(b => b.Category)
                                            .Include(b => b.BookGallery)
                                            .FirstOrDefaultAsync();
            return book;
        }
    }
}