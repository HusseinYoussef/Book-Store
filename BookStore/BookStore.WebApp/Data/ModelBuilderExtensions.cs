using System;
using System.Collections.Generic;
using BookStore.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.WebApp.Data
{
    public static class ModelBuilderExtensions
    {
        public static void seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id=1, Name="History"
                },
                new Category()
                {
                    Id=2, Name="Fantasy"
                },
                new Category()
                {
                    Id=3, Name="Romance"
                },
                new Category()
                {
                    Id=4, Name="Thriller"
                },
                new Category()
                {
                    Id=5, Name="Adventure"
                },
                new Category()
                {
                    Id=6, Name="ScienceFiction"
                },
                new Category()
                {
                    Id=7, Name="Mystery"
                },
                new Category()
                {
                    Id=8, Name="Development"
                },
                new Category()
                {
                    Id=9, Name="Guide"
                },
                new Category()
                {
                    Id=10, Name="Art"
                },
                new Category()
                {
                    Id=11, Name="Childrens"
                }
            );

            modelBuilder.Entity<Language>().HasData(
                new Language()
                {
                    Id=1, Name="English"
                },
                new Language()
                {
                    Id=2, Name="Arabic"
                },
                new Language()
                {
                    Id=3, Name="French"
                },
                new Language()
                {
                    Id=4, Name="Italian"
                },
                new Language()
                {
                    Id=5, Name="German"
                },
                new Language()
                {
                    Id=6, Name="Chinese"
                },
                new Language()
                {
                    Id=7, Name="Japanese"
                }
            );
        }
    }
}