using BookApi.Model.DTO;
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
        public ActionResult<IEnumerable<TeacherDTO>> GetTeachers()
        {
            var teachers = _repository.GetTeachers();
            var listOfTeachers = new List<TeacherDTO>();

            // use automapper
            foreach (var teacher in teachers)
            {
                var newTeacher = new TeacherDTO
                {
                    Name = teacher.FirstName + teacher.LastName,
                    Id = teacher.Id,
                    MainCategory = teacher.MainCategory
                };
                listOfTeachers.Add(newTeacher);
            }

            //if (!teachers.Any())
            //    return NotFound();
            // if the collection is empty, dont return a 404 not found

            return Ok(listOfTeachers);
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
