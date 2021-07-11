using AutoMapper;
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
        public ActionResult<IEnumerable<TeacherDTO>> GetTeachers()
        {
            var teachers = _repository.GetTeachers();
            var listOfTeachers = _mapper.Map<IEnumerable<TeacherDTO>>(teachers);

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


            return Ok(_mapper.Map<TeacherDTO>(teacher));
        }
    }
}
