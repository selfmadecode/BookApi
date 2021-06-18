using AutoMapper;
using BookApi.Model.Interfaces;
using BookApi.Model.ViewModels;
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
        public void AddBook(BookVM book)
        {
            var newBook = _mapper.Map<Book>(book);
            _context.Add(newBook);
            _context.SaveChanges();
        }

        public BookVM GetBookById(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return null;

            return _mapper.Map<BookVM>(book);            
        }

        public IEnumerable<BookVM> GetBooks()
            => _mapper.Map<IEnumerable<BookVM>>(_context.Books.ToList());
    }
}
