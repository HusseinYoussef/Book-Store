using System;
using System.Collections.Generic;
using BookStore.WebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.WebApp.Data
{
    public class BookStoreDbContext : IdentityDbContext<User>
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {}

        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Gallery> BooksGalleries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                        .HasIndex(book => book.Title)
                        .IsUnique();

            modelBuilder.Entity<Language>()
                        .HasMany(l => l.Books)
                        .WithOne(b => b.Language)
                        .OnDelete(DeleteBehavior.SetNull);

            // Book - Category == Many to Many
            modelBuilder.Entity<Book>()
                        .HasMany(b => b.Category)
                        .WithMany(c => c.Book);

            // Book - Gallery == one to many
            modelBuilder.Entity<Book>()
                        .HasMany(b => b.BookGallery)
                        .WithOne(p => p.Book)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                        .HasOne(b => b.User)
                        .WithMany(u => u.Books)
                        .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}