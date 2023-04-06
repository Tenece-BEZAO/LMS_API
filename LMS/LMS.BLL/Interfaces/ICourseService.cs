using LMS.BLL.DTOs.Request;
using LMS.BLL.DTOs.Response;
using LMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Interfaces
{
    public interface ICourseService
    {
        Task<CourseDto> CreateCourse(CreateCourseDto course);
        Task<CourseDto> EditCourse(EditCourseDto editCourse);
        Task<CourseDto> GetCourseById(int courseId);
        Task<IEnumerable<Course>> GetAllCourse();
        Task<bool> DeleteCourse(int id);
        Task<EnrolledStudentsCourses> EnrollForACourse(CourseEnrollDto courseEnrollDto);
        Task<IEnumerable<Course>> GetUserCompletedCourses(int userId);
        Task<IEnumerable<Course>> GetAllCompletedCourses();
        Task<bool> MarkAsComplete(CourseEnrollDto markCourseAsCompleted);
       Task<IEnumerable<Course>> GetUserEnrolledCourses(int studentId);



    }
}
