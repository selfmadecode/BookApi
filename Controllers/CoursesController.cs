using BookApi.Model;
using BookApi.Model.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Controllers
{
    [Route("api/[controller]/{teacherId}")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _repository;

        public CoursesController(ICourseLibraryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository)); ;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Course>> GetAuthorCourses(Guid teacherId)
        {
            var teacher = _repository.GetTeacher(teacherId);

            if (teacher == null)
                return NotFound();

            var courses = _repository.GetCourses(teacherId);
            return Ok(courses);
        }
    }
}
