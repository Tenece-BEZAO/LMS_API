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
        Task<Course> EditCourse(EditCourseDto editCourse);
        Task<CourseDto> GetCourseById(int courseId);

        Task<IEnumerable<Course>> GetAllCourse();
        Task<bool> DeleteCourse(int id);

    }
}
