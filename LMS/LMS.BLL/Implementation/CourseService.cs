using LMS.BLL.DTOs.Request;
using LMS.BLL.DTOs.Response;
using LMS.BLL.Exceptions;
using LMS.BLL.Interfaces;
using LMS.DAL;
using LMS.DAL.Entities;
using LMS.Repository;
using Microsoft.EntityFrameworkCore;
using NotImplementedException = LMS.BLL.Exceptions.NotImplementedException;
using AutoMapper;

namespace LMS.BLL.Implementation;

public class CourseService : ICourseService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Course> _courseRepo;
    private readonly IRepository<Instructor> _instructorRepo;
    private readonly IRepository<Student> _studentRepo;
    private readonly IRepository<EnrolledStudentsCourses> _enrolledRepo;
    private readonly IRepository<CompletedStudentsCourses> _completedRepo;
    private readonly LMSAppDbContext _dbContext;
    private readonly IMapper _mapper;

    public CourseService(IUnitOfWork unitOfWork, LMSAppDbContext dbContext, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _dbContext = dbContext;
        _mapper = mapper;
        _courseRepo = _unitOfWork.GetRepository<Course>();
        _instructorRepo = _unitOfWork.GetRepository<Instructor>();
        _studentRepo = _unitOfWork.GetRepository<Student>();
        _enrolledRepo = _unitOfWork.GetRepository<EnrolledStudentsCourses>();
        _completedRepo = _unitOfWork.GetRepository<CompletedStudentsCourses>();
    }

    public async Task<CourseDto> CreateCourse(CreateCourseDto course)
    {
        var Instructor = await _instructorRepo.GetByIdAsync(course.InstructorId);
        if (Instructor == null)
            throw new NotFoundException("Invalid instructor Id");

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

    public async Task<CourseDto> EditCourse(EditCourseDto editCourse)
    {
        var Instructor = await _instructorRepo.GetByIdAsync(editCourse.InstructorId);
        if (Instructor == null)
            throw new NotFoundException("Invalid instructor Id");

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
        foundCourse.IsActive = editCourse.IsActive;


        Course updatedCourse = await _courseRepo.UpdateAsync(foundCourse);
        if (updatedCourse == null)
            throw new NotImplementedException("Unable to update course");

        return new CourseDto()
        {
            Title = updatedCourse.Title,
            Detail = updatedCourse.Detail,
            HeaderImageUrl = updatedCourse.HeaderImageUrl,
            Price = updatedCourse.Price,
            VideoResourceUrl = updatedCourse.VideoResourceUrl,
            TextResourceUrl = updatedCourse.TextResourceUrl,
            AdditionalResourcesUrl = updatedCourse.AdditionalResourcesUrl,
            CourseType = updatedCourse.CourseType,
            InstructorId = updatedCourse.InstructorId,
            IsActive = updatedCourse.IsActive
        };
    }

    public async Task<EnrolledStudentsCourses> EnrollForACourse(CourseEnrollDto courseEnrollDto)
    {
        var student = await _studentRepo.GetByIdAsync(courseEnrollDto.StudentId);
        if (student == null)
            throw new NotFoundException("Invalid user id ");
        var course = await _courseRepo.GetByIdAsync(courseEnrollDto.CourseId);
        if (course == null)
            throw new NotFoundException("Invalid course id");

        var alreadyEnrolled = await _enrolledRepo.GetByAsync(c =>
            c.CourseId == courseEnrollDto.CourseId && c.StudentId == courseEnrollDto.StudentId);
        if (alreadyEnrolled.Count() > 0)
            throw new BadRequestException("You have already enrolled for this course");

        EnrolledStudentsCourses newEnrollCourse = new EnrolledStudentsCourses()
        {
            CourseId = courseEnrollDto.CourseId,
            StudentId = courseEnrollDto.StudentId,
            CreatedBy = student.FullName
        };


        var result = await _enrolledRepo.AddAsync(newEnrollCourse);
        if (result != null)
        {
            return result;
        }

        throw new NotImplementedException("Was not able to enroll for this course");
    }

    public async Task<IEnumerable<Course>> GetAllCompletedCourses()
    {
        var isCompleted = await _completedRepo.GetAllAsync();

        IEnumerable<Course> courses;
        foreach (var enrolledCourse in isCompleted)
        {
            courses = await _courseRepo.GetByAsync(c => c.Id == enrolledCourse.CourseId);

            if (courses.Count() > 0)
            {
                return courses;
            }
        }

        throw new NotFoundException("No course was found");
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
            throw new NotFoundException("No course was found");

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

    public Task<IEnumerable<Course>> GetUserCompletedCourses(int userId)
    {
        throw new System.NotImplementedException();
    }

    public async Task<IEnumerable<CourseDto>> GetUserEnrolledCourses(int studentId)
    {
        var result = await _studentRepo.GetSingleByAsync(s => s.Id == studentId,
            include: x => x.Include(x => x.EnrolledCourses).ThenInclude(x => x.Course));

        var response = result.EnrolledCourses.Select(x => x.Course).ToList();

        var mappedValue = _mapper.Map<IEnumerable<CourseDto>>(response);
        return mappedValue;
        /* IEnumerable<Course> courses;
         foreach (var enrolledCourse in enrolledCourses)
         {
             courses = await _courseRepo.GetByAsync(c => c.Id == enrolledCourse.CourseId);

             if (courses.Count() > 0)
             {
                 return courses;
             }
         }
         throw new NotFoundException("No course was found");*/
        //hrow new System.NotImplementedException();
    }

    public async Task<bool> MarkAsComplete(CourseEnrollDto markCourseAsCompleted)
    {
        var enrolledCourse = await _enrolledRepo.GetByAsync(c =>
            c.CourseId == markCourseAsCompleted.CourseId && c.StudentId == markCourseAsCompleted.StudentId);
        if (enrolledCourse.Count() == 0)
            throw new NotFoundException("Course was not found");
        var student = await _studentRepo.GetByIdAsync(markCourseAsCompleted.StudentId);


        var already = await _completedRepo.GetByAsync(c =>
            c.CourseId == markCourseAsCompleted.CourseId && c.StudentId == markCourseAsCompleted.StudentId);
        if (already.Count() != 0)
            throw new NotImplementedException("This course have already been marked as complete");

        var completedCourse = new CompletedStudentsCourses()
        {
            CourseId = markCourseAsCompleted.CourseId,
            StudentId = markCourseAsCompleted.StudentId,
            CreatedBy = student.FullName
        };

        var created = await _completedRepo.AddAsync(completedCourse);
        if (created == null)
            throw new NotImplementedException("Was ubale to complete course");


        return true;
    }

    Task<IEnumerable<Course>> ICourseService.GetUserEnrolledCourses(int studentId)
    {
        throw new System.NotImplementedException();
    }
}
