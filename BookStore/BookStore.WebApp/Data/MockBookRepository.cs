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
                new Book() {Id=1, Title="MVC", Author="Hassan", Description="This is the description of MVC Book", Category="Development", Language="Chinese", TotalPages=543},
                new Book() {Id=2, Title="C#", Author="Hussein", Description="This is the description of C# Book", Category="Programming", Language="English", TotalPages=400},
                new Book() {Id=3, Title="C++", Author="Negan", Description="This is the description of C++ Book", Category="Programming", Language="English", TotalPages=250},
                new Book() {Id=4, Title="Database", Author="R Jay", Description="This is the description of Database Book", Category="Design", Language="Chinese", TotalPages=287},
                new Book() {Id=5, Title="Azure", Author="Morgan Slizern", Description="This is the description of Azure DevOps Book", Category="Cloud", Language="Chinese", TotalPages=150}
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