using LMS.BLL.DTOs.Request;
using LMS.BLL.DTOs.Response;
using LMS.BLL.Exceptions;
using LMS.BLL.Interfaces;
using LMS.DAL.Entities;
using LMS.Repository;
using NotImplementedException = LMS.BLL.Exceptions.NotImplementedException;

namespace LMS.BLL.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Course> _courseRepo;


        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _courseRepo = _unitOfWork.GetRepository<Course>();
        }
        public async Task<CourseDto> CreateCourse(CreateCourseDto course)
        {
            Course newCourse = new Course()
            {
                Title = course.Title,
                Detail = course.Detail,
                HeaderImageUrl = course.HeaderImageUrl,
                Price = course.Price,
                VideoResourceUrl = course.VideoResourceUrl,
                TextResourceUrl = course.TextResourceUrl,
                AdditionalResourcesUrl = course.AdditionalResourcesUrl,
                CourseType = course.CourseType,
                InstructorId = course.InstructorId,
                IsActive = false,

            };
            var createdCourse = await _courseRepo.AddAsync(newCourse);
            if (createdCourse == null)
                throw new NotImplementedException("Course was not able to be created");

            return new CourseDto()
            {

                Title = createdCourse.Title,
                Detail = createdCourse.Detail,
                HeaderImageUrl = createdCourse.HeaderImageUrl,
                Price = createdCourse.Price,
                VideoResourceUrl = createdCourse.VideoResourceUrl,
                TextResourceUrl = createdCourse.TextResourceUrl,
                AdditionalResourcesUrl = createdCourse.AdditionalResourcesUrl,
                CourseType = createdCourse.CourseType,
                InstructorId = createdCourse.InstructorId,
                IsActive = false,
            };
        }

        public async Task<bool> DeleteCourse(int id)
        {
            var course = await _courseRepo.GetByIdAsync(id);
            if (course == null)
                throw new NotFoundException("Course not found");
            await _courseRepo.DeleteAsync(course);
            return true;
        }

        public async Task<Course> EditCourse(EditCourseDto editCourse)
        {
            var foundCourse = await _courseRepo.GetByIdAsync(editCourse.Id);
            if (foundCourse == null)
                throw new NotFoundException("Course not found");

            foundCourse.Title = editCourse.Title;
            foundCourse.Detail = editCourse.Detail;
            foundCourse.HeaderImageUrl = editCourse.HeaderImageUrl;
            foundCourse.Price = editCourse.Price;
            foundCourse.VideoResourceUrl = editCourse.VideoResourceUrl;
            foundCourse.TextResourceUrl = editCourse.TextResourceUrl;
            foundCourse.AdditionalResourcesUrl = editCourse.AdditionalResourcesUrl;
            foundCourse.CourseType = editCourse.CourseType;
            foundCourse.InstructorId = editCourse.InstructorId;
            foundCourse.IsActive = editCourse.IsActive;



            Course updatedCourse = await _courseRepo.UpdateAsync(foundCourse);
            if (updatedCourse == null)
                throw new NotImplementedException("Unable to update course");

            return updatedCourse;

        }

        public async Task<IEnumerable<Course>> GetAllCourse()
        {
            var courses = await _courseRepo.GetAllAsync();
            if (courses == null)
                throw new NotImplementedException("No courses");

            return courses;
        }

        public async Task<CourseDto> GetCourseById(int courseId)
        {
            var course = await _courseRepo.GetByIdAsync(courseId);
            if (course == null)
                throw new NotImplementedException("No courses");

            return new CourseDto()
            {
                Title = course.Title,
                Detail = course.Detail,
                HeaderImageUrl = course.HeaderImageUrl,
                Price = course.Price,
                VideoResourceUrl = course.VideoResourceUrl,
                TextResourceUrl = course.TextResourceUrl,
                AdditionalResourcesUrl = course.AdditionalResourcesUrl,
                CourseType = course.CourseType,
                InstructorId = course.InstructorId,
                IsActive = course.IsActive
            };
        }
    }
}
