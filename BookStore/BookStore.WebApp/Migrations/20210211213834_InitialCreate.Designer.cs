﻿// <auto-generated />
using System;
using BookStore.WebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BookStore.WebApp.Migrations
{
    [DbContext(typeof(BookStoreDbContext))]
    [Migration("20210211213834_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("BookCategory", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.HasKey("BookId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("BookCategory");
                });

            modelBuilder.Entity("BookStore.WebApp.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("LanguageId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("TotalPages")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookStore.WebApp.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "History"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Fantasy"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Romance"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Thriller"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = 6,
                            Name = "ScienceFiction"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Mystery"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Development"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Guide"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Art"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Childrens"
                        });
                });

            modelBuilder.Entity("BookStore.WebApp.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "English"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Arabic"
                        },
                        new
                        {
                            Id = 3,
                            Name = "French"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Italian"
                        },
                        new
                        {
                            Id = 5,
                            Name = "German"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Chinese"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Japanese"
                        });
                });

            modelBuilder.Entity("BookCategory", b =>
                {
                    b.HasOne("BookStore.WebApp.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookStore.WebApp.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookStore.WebApp.Models.Book", b =>
                {
                    b.HasOne("BookStore.WebApp.Models.Language", "Language")
                        .WithMany("Books")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("BookStore.WebApp.Models.Language", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
