using BookApi.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Model.Interfaces
{
    public interface IBook
    {
        Book AddBook(BookVM book);
        IEnumerable<Book> GetBooks();
        Book GetBookById(int id);
        Book UpdateBook(int id, BookVM book);
        void DeleteBookById(int id);

    }
}
