using BookApi.Model.Interfaces;
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
    public class TeachersController : ControllerBase
    {
        private readonly ICourseLibraryRepository _repository;

        public TeachersController(ICourseLibraryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository)); ;
        }

        [HttpGet]
        public IActionResult GetTeachers()
        {
            var teachers = _repository.GetTeachers();

            //if (!teachers.Any())
            //    return NotFound();
            // if the collection is empty, dont return a 404 not found

            return Ok(teachers);
        }

        [HttpGet("{teacherId}")]
        public IActionResult GetTeacher(Guid teacherId)
        {
            var teacher = _repository.GetTeacher(teacherId);

            if (teacher == null)
                return NotFound();

            return Ok(teacher);
        }
    }
}
