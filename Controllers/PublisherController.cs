using BookApi.Model.Interfaces;
using BookApi.Model.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisher _publisher;

        public PublisherController(IPublisher publisher)
        {
            _publisher = publisher;
        }

        [HttpPost]
        public IActionResult AddPublisher(PublisherVM publisher)
        {
            var newPublisher = _publisher.AddPublisher(publisher);
            return Created(nameof(AddPublisher), newPublisher);
        }

        [HttpGet("{publisherId}")]
        public IActionResult GetPublisherWithBooks(int publisherId)
        {
            var response = _publisher.GetPublisherWithBook(publisherId);

            if (response == null) return NotFound();

            return Ok(response);
        }
    }
}
