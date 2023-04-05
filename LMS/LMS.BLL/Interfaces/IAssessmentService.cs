using LMS.BLL.DTOs.Request;
using LMS.BLL.DTOs.Response;

namespace LMS.BLL.Interfaces;

public interface IAssessmentService
{
    Task<Status> CreateAssessment(AssessmentRequestDto assessmentRequest);
}
