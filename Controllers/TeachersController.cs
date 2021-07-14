using AutoMapper;
using BookApi.Model;
using BookApi.Model.DTO;
using BookApi.Model.Interfaces;
using BookApi.Model.Services.Helpers;
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
        private readonly IMapper _mapper;

        public TeachersController(ICourseLibraryRepository repository, IMapper mapper)
        {
            if (mapper is null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;            
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<TeacherDTO>> GetTeachers([FromRoute] DataFilters filters)
        {
            var teachers = _repository.GetTeachers(filters);
            var listOfTeachers = _mapper.Map<IEnumerable<TeacherDTO>>(teachers);

            //if (!teachers.Any())
            //    return NotFound();
            // if the collection is empty, dont return a 404 not found

            return Ok(listOfTeachers);
        }

        [HttpGet("{teacherId}", Name ="GetTeacher")]
        public IActionResult GetTeacher(Guid teacherId)
        {
            var teacher = _repository.GetTeacher(teacherId);

            if (teacher == null)
                return NotFound();


            return Ok(_mapper.Map<TeacherDTO>(teacher));
        }

        [HttpPost]
        public ActionResult<TeacherDTO> CreateTeacher(CreateTeacherDTO teacher)
        {
            var newTeacher = _mapper.Map<Teacher>(teacher);
            _repository.AddTeacher(newTeacher);
            _repository.Save();

            var teacherToReturn = _mapper.Map<TeacherDTO>(newTeacher);

            //return a response in the location header containing the URL where the created
            //resource lives
            return CreatedAtRoute("GetTeacher",
                new { teacherId = teacherToReturn.Id }, teacherToReturn);
        }

        [HttpOptions]
        public IActionResult GetTeachersOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }

        [HttpDelete("{teacherId}")]
        public IActionResult DeleteTeacher(Guid teacherId)
        {
            var teacherFromRepo = _repository.GetTeacher(teacherId);

            if (teacherFromRepo == null)
                return NotFound();

            _repository.DeleteTeacher(teacherFromRepo);
            _repository.Save();

            return NoContent();
        }
    }
}
