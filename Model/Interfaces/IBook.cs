using BookApi.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Model.Interfaces
{
    public interface IBook
    {
        void AddBook(BookVM book);
        IEnumerable<BookVM> GetBooks();
        BookVM GetBookById(int id);
    }
}
