using AutoMapper;
using LMS.BLL.DTOs.Request;
using LMS.BLL.DTOs.Response;
using LMS.BLL.Interfaces;
using LMS.DAL.Entities;
using LMS.DAL.Entities.identityEntities;
using LMS.Repository;
using Microsoft.AspNetCore.Identity;

namespace LMS.BLL.Implementation;

public class AssessmentService : IAssessmentService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;
    private readonly IRepository<Assessment> _assessmentRepository;
    private readonly IRepository<CompletedStudentsAssessment> _completedAssessment;
    private readonly IRepository<Course> _courseRepository;

    public AssessmentService(IMapper mapper, IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _assessmentRepository = _unitOfWork.GetRepository<Assessment>();
        _completedAssessment = _unitOfWork.GetRepository<CompletedStudentsAssessment>();
        _courseRepository = _unitOfWork.GetRepository<Course>();
    }

    public async Task<Status> CreateAssessment(AssessmentRequestDto assessmentRequest)
    {
        var status = new Status();
        var courseExists = await _courseRepository.GetByIdAsync(assessmentRequest.CourseId);

        if (courseExists is null)
        {
            status.StatusCode = 0;
            status.Message = "Course not found";

            return status;
        }

        // var newAssessment = _mapper.Map<Assessment>(assessmentRequest);
        var newAssessment = new Assessment()
        {
            Title = assessmentRequest.Title,
            AssessmentType = assessmentRequest.AssessmentType,
            Score = assessmentRequest.Score,
            StudentId = assessmentRequest.StudentId,
            InstructorId = assessmentRequest.InstructorId,
            CourseId = assessmentRequest.CourseId
        };
        var createdAssessment = await _assessmentRepository.AddAsync(newAssessment);

        if (createdAssessment is not null)
        {
            status.StatusCode = 1;
            status.Message = "New assessment was successfully created";

            return status;
        }

        status.StatusCode = 0;
        status.Message = "Something went wrong. Was unable to create assessment";

        return status;
    }

    public async Task<IEnumerable<Assessment>> GetAssessments()
    {
        var status = new Status();
        var assessments = await _assessmentRepository.GetAllAsync();

        if (assessments is null)
        {
            status.StatusCode = 0;
            status.Message = "Assessment not found";
        }

        return assessments;
    }

    public async Task<Assessment> GetAssessment(int id)
    {
        var status = new Status();
        var assessment = await _assessmentRepository.GetByIdAsync(id);

        if (assessment is null)
        {
            status.StatusCode = 0;
            status.Message = "Assessment not found";
        }

        return assessment;
    }

    public async Task<Status> UpdateAssessment(EditAssessmentDto requestDto)
    {
        var status = new Status();
        var course = await _courseRepository.GetByIdAsync(requestDto.CourseId);

        if (course is null)
        {
            status.StatusCode = 0;
            status.Message = "Course not found.";

            return status;
        }

        var assessment = await _assessmentRepository.GetByIdAsync(requestDto.EditAssessmentId);

        if (assessment is null)
        {
            status.StatusCode = 0;
            status.Message = "Assessment not found";
        }

        assessment.Title = requestDto.Title;
        assessment.AssessmentType = requestDto.AssessmentType;
        assessment.Score = requestDto.Score;
        assessment.StudentId = requestDto.StudentId;
        assessment.InstructorId = requestDto.InstructorId;
        assessment.CourseId = requestDto.CourseId;

        var updatedAssessment = await _assessmentRepository.UpdateAsync(assessment);

        if (updatedAssessment is null)
        {
            status.StatusCode = 0;
            status.Message = "Was unable to update assessment";

            return status;
        }

        status.StatusCode = 1;
        status.Message = "Assessment update was successful";

        return status;
    }

    public async Task<bool> DeleteAssessment(int id)
    {
        var status = new Status();
        var assessment = await _assessmentRepository.GetByIdAsync(id);

        if (assessment is null)
        {
            status.StatusCode = 0;
            status.Message = "Assessment not found";
        }

        await _assessmentRepository.DeleteAsync(assessment);

        return true;
    }

    public async Task<Assessment?> GetEnrolledAssessmentforAStudent(string studentId)
    {
        var status = new Status();

        int studentIdInt = int.Parse(studentId);
        var enrolledAssessment = await _assessmentRepository.GetByIdAsync(studentIdInt);

        if (enrolledAssessment is null)
        {
            status.StatusCode = 0;
            status.Message = "Enrolled Assessment not found";
        }

        return enrolledAssessment;
    }

    public async Task<IEnumerable<Assessment>> GetAllCompletedAssessment()
    {
        var status = new Status();
        var completedAssessments = await _completedAssessment.GetAllAsync();

        if (completedAssessments is null)
        {
            status.StatusCode = 0;
            status.Message = "Completed Assessments not found";
        }

        return _mapper.Map<IEnumerable<Assessment>>(completedAssessments);
    }
}
