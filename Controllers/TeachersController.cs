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
using System.Text.Json;
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

        [HttpGet(Name = "GetTeachers")]
        [HttpHead]
        public ActionResult<IEnumerable<TeacherDTO>> GetTeachers([FromQuery] TeacherResourceParameters filters)
        {
            var teachersFromRepo = _repository.GetTeachers(filters);

            //creating meta data
            var previousPage = teachersFromRepo.HasPrevious ?
                CreateTeachersResourceUri(filters, ResourceUriType.PreviousPage) : null;

            var nextPage = teachersFromRepo.HasNext ?
                CreateTeachersResourceUri(filters, ResourceUriType.NextPage) : null;


            var paginationMetaData = new
            {
                totalCount = teachersFromRepo.TotalCount,
                pageSize = teachersFromRepo.PageSize,
                currentPage = teachersFromRepo.CurrentPage,
                totalPages = teachersFromRepo.TotalPages,
                previousPage,
                nextPage
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

            var listOfTeachers = _mapper.Map<IEnumerable<TeacherDTO>>(teachersFromRepo);

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

            var teacherDto = _mapper.Map<TeacherDTO>(teacher);

            return Ok(CreateLinksForTeachers(teacherDto));
        }

        [HttpPost]
        public ActionResult<TeacherDTO> CreateTeacher(CreateTeacherDTO teacher)
        {
            var newTeacher = _mapper.Map<Teacher>(teacher);
            _repository.AddTeacher(newTeacher);
            _repository.Save();

            var teacherToReturn = _mapper.Map<TeacherDTO>(newTeacher);
            teacherToReturn = CreateLinksForTeachers(teacherToReturn);

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

        [HttpDelete("{teacherId}", Name ="DeleteTeacher")]
        public IActionResult DeleteTeacher(Guid teacherId)
        {
            var teacherFromRepo = _repository.GetTeacher(teacherId);

            if (teacherFromRepo == null)
                return NotFound();

            _repository.DeleteTeacher(teacherFromRepo);
            _repository.Save();

            return NoContent();
        }

        private string CreateTeachersResourceUri(TeacherResourceParameters param, ResourceUriType type)
        {
            // creating a pagination info for metadata passed to the response header
            // when calling the GetTeachers method
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetTeachers", new
                    {
                        pageNumber = param.PageNumber - 1,
                        pageSize = param.PageSize,
                        searchParam = param.SearchParam,
                        orderBy = param.OrderBy
                    });

                case ResourceUriType.NextPage:
                    return Url.Link("GetTeachers", new
                    {
                        pageNumber = param.PageNumber + 1,
                        pageSize = param.PageSize,
                        searchParam = param.SearchParam,
                        orderBy = param.OrderBy
                    });

                default:
                    return Url.Link("GetTeachers", new
                    {
                        pageNumber = param.PageNumber,
                        pageSize = param.PageSize,
                        searchParam = param.SearchParam,
                        orderBy = param.OrderBy
                    });
            }
        }

        private TeacherDTO CreateLinksForTeachers(TeacherDTO teacher)
        {
            var id = teacher.Id;

             teacher.Links.Add(
                new LinkDTO(Url.Link("GetTeacher",
                new { teacherId = id }), "self", "GET"));

            teacher.Links.Add(new LinkDTO(Url.Link("DeleteTeacher",
                new { teacherId = id }), "delete_teacher", "DELETE"));

            teacher.Links.Add(new LinkDTO(Url.Link("CreateCourseForTeacher",
                new { teacherId = id}), "create_course_for_teacher", "POST"));

            teacher.Links.Add(new LinkDTO(Url.Link("GetAuthorCourses",
                new { teacherId = id}), "get_courses_for_teacher", "GET"));

            return teacher;
        }
    }
}
