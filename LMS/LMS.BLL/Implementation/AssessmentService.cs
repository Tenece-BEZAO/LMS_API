using AutoMapper;
using LMS.BLL.DTOs.Request;
using LMS.BLL.DTOs.Response;
using LMS.BLL.Interfaces;
using LMS.DAL.Entities;
using LMS.DAL.Entities.identityEntities;
using LMS.DAL.Repository;
using Microsoft.AspNetCore.Identity;

namespace LMS.BLL.Implementation;

public class AssessmentService : IAssessmentService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;
    private readonly IRepository<Assessment> _assessmentRepository;

    public AssessmentService(IMapper mapper, IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _assessmentRepository = _unitOfWork.GetRepository<Assessment>();
    }

    public async Task<Status> CreateAssessment(AssessmentRequestDto assessmentRequest)
    {
        var status = new Status();
        var courseExists = await _assessmentRepository.GetByIdAsync(assessmentRequest.CourseId);

        if (courseExists is null)
        {
            status.StatusCode = 0;
            status.Message = "Course not found";

            return status;
        }

        var newAssessment = _mapper.Map<Assessment>(assessmentRequest);
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
}
