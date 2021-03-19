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
            IEnumerable<Book> books = await _context.Books.AsNoTracking()
                                                    .Select(b => new Book() {Id=b.Id, Title=b.Title, Description=b.Description, Author=b.Author, CoverPhotoPath=b.CoverPhotoPath})
                                                    .ToListAsync();
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
                                                    .Select(b => new Book() {Id=b.Id, Title=b.Title, Description=b.Description, Author=b.Author, CoverPhotoPath=b.CoverPhotoPath})
                                                    .ToListAsync();
            return books;
        }

        public async Task<IEnumerable<Book>> GetTopBooks(int count=3)
        {
            IEnumerable<Book> books = await _context.Books.AsNoTracking()
                                                    .OrderBy(b => b.Id).Take(count)
                                                    .Select(b => new Book() {Id=b.Id, Title=b.Title, Description=b.Description, Author=b.Author, CoverPhotoPath=b.CoverPhotoPath})
                                                    .ToListAsync();
            return books;
        }

        public async Task<IEnumerable<Book>> SearchByTitle(string bookTitle)
        {
            IEnumerable<Book> books = await _context.Books.AsNoTracking()
                                                    .Where(b => b.Title.ToLower().Contains(bookTitle.ToLower()))
                                                    .Select(b => new Book() {Id=b.Id, Title=b.Title, Description=b.Description, Author=b.Author, CoverPhotoPath=b.CoverPhotoPath})
                                                    .ToListAsync();
            return books;
        }

        public async Task<IEnumerable<Book>> SearchByCategory(string bookCategory)
        {
            IEnumerable<Book> books = await _context.Books.AsNoTracking()
                                                    .Where(b => b.Category.Select(c => c.Name.ToLower()).Contains(bookCategory.ToLower()))
                                                    .Select(b => new Book(){Id=b.Id, Title=b.Title, Description=b.Description, Author=b.Author, CoverPhotoPath=b.CoverPhotoPath})
                                                    .ToListAsync();
            return books;
        }
    }
}