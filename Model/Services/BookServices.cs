﻿using AutoMapper;
using BookApi.Model.Entities;
using BookApi.Model.Interfaces;
using BookApi.Model.ViewModels;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Model.Services
{
    public class BookServices : IBook
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BookServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Book AddBookWithPublisherAndAuthors(BookVM book)
        {
            // use dbContext transactions
            var newBook = _mapper.Map<Book>(book);


            using (IDbContextTransaction dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Add(newBook);

                    foreach (var Id in book.AuthorIds)
                    {
                        var book_authors = new Book_Author
                        {
                            BookId = newBook.Id,
                            AuthorId = Id,

                        };
                        _context.Books_Authors.Add(book_authors);
                    }
                    _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {

                    dbContextTransaction.Rollback();
                }
            }

            return newBook;
        }

        public void DeleteBookById(int id)
        {
            var bookToBeDeleted = _context.Books.FirstOrDefault(b => b.Id == id);

            if (bookToBeDeleted != null)
            {
                _context.Books.Remove(bookToBeDeleted);
                _context.SaveChanges();
            }
        }

        public Book GetBookById(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return null;

            return book;            
        }

        public IEnumerable<Book> GetBooks()
            => _context.Books.ToList();

        public Book UpdateBook(int id, BookVM book)
        {
            var oldBook = _context.Books.FirstOrDefault(b => b.Id == id);

            if(oldBook != null)
            {
                _mapper.Map<BookVM, Book>(book, oldBook);
                _context.SaveChanges();
            }

            return oldBook;
        }
    }
}
