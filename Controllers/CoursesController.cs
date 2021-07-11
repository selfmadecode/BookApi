using AutoMapper;
using BookApi.Model;
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
    [Route("api/teachers/{teacherId}/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _repository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseDTO>> GetAuthorCourses(Guid teacherId)
        {

            if (!_repository.TeacherExists(teacherId))
                return NotFound();

            var courses = _repository.GetCourses(teacherId);

            return Ok(_mapper.Map<IEnumerable<CourseDTO>>(courses));
        }

        [HttpGet("{courseId}")]
        public ActionResult<CourseDTO> GetAuthorCourse(Guid courseId, Guid teacherId)
        {
            if (!_repository.TeacherExists(teacherId))
                return NotFound();

            var course = _repository.GetCourse(teacherId, courseId);

            if (course == null)
                return NotFound();

            return Ok(_mapper.Map<CourseDTO>(course));
        }
    }
}
