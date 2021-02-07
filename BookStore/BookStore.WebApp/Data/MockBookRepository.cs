using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.WebApp.Models;

namespace BookStore.WebApp.Data
{
    public class MockBookRepository : IBookRepository
    {
        private List<Book> DataSource()
        {
            return new List<Book>()
            {
                new Book() {Id=1, Title="MVC", Author="Hassan", Description="This is the description of MVC Book", Categories=new List<string>{"Development", "Guide"}, Language=BookLanguage.English, TotalPages=543},
                new Book() {Id=2, Title="C#", Author="Hussein", Description="This is the description of C# Book", Categories=new List<string>{"Guide"}, Language=BookLanguage.Arabic, TotalPages=400},
                new Book() {Id=3, Title="C++", Author="Negan", Description="This is the description of C++ Book", Categories=new List<string>{"Guide"}, Language=BookLanguage.Chinese, TotalPages=250},
                new Book() {Id=4, Title="Database", Author="R Jay", Description="This is the description of Database Book", Categories=new List<string>{"Development"}, Language=BookLanguage.English, TotalPages=287},
                new Book() {Id=5, Title="Azure", Author="Morgan Slizern", Description="This is the description of Azure DevOps Book", Categories=new List<string>{"Development"}, Language=BookLanguage.German, TotalPages=150},
                new Book() {Id=6, Title="Harry Potter", Author="Hans Zimmer", Description="This is the Harry Books Trilogy", Categories=new List<string>{"Adventure", "Fantasy"}, Language=BookLanguage.Chinese, TotalPages=723}
            };
        }

        public Book AddBook(Book newBook)
        {
            return newBook;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return this.DataSource().ToList();
        }

        public Book GetBookById(int id)
        {
            return this.DataSource().FirstOrDefault(b => b.Id==id);
        }
    }
}