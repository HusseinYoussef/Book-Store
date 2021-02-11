using System;
using System.Collections.Generic;
using BookStore.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.WebApp.Data
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {}

        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                        .HasIndex(book => book.Title)
                        .IsUnique();

            modelBuilder.Entity<Language>()
                        .HasMany(l => l.Books)
                        .WithOne(b => b.Language)
                        .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Book>()
                        .HasMany(b => b.Category)
                        .WithMany(c => c.Book);

            modelBuilder.seed();
        }
    }
}