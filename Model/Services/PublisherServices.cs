using BookApi.Model.Entities;
using BookApi.Model.Interfaces;
using BookApi.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Model.Services
{
    public class PublisherServices : IPublisher
    {
        private readonly ApplicationDbContext _dbContext;

        public PublisherServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Publisher AddPublisher(PublisherVM publisher)
        {
            var newPublisher = new Publisher
            {
                Name = publisher.Name
            };

            _dbContext.Publishers.Add(newPublisher);
            _dbContext.SaveChanges();

            return newPublisher;
        }

        public PublisherWithBookVM GetPublisherWithBook(int id)
        => _dbContext.Publishers.Where(i => i.Id == id)
            .Select(book => new PublisherWithBookVM
            {
                Name = book.Name,
                BooksPublished = book.Books.Select(n => n.Title).ToList()
            }).FirstOrDefault();
    }
}
