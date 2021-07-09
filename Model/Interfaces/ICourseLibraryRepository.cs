using System;
using System.Collections.Generic;
using BookApi.Model.Entities;

namespace BookApi.Model.Interfaces
{
    public interface ICourseLibraryRepository
    {    
        IEnumerable<Course> GetCourses(Guid teacherId);
        Course GetCourse(Guid teacherId, Guid courseId);
        void AddCourse(Guid teacherId, Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(Course course);
        IEnumerable<Teacher> GetTeachers();
        Teacher GetTeacher(Guid teacherId);
        IEnumerable<Teacher> GetTeachers(IEnumerable<Guid> teacherIds);
        void AddTeacher(Teacher teacher);
        void DeleteTeacher(Teacher teacher);
        void UpdateTeacher(Teacher teacher);
        bool TeacherExists(Guid teacherId);
        bool Save();
    }
}
