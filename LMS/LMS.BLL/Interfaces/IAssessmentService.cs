using LMS.BLL.DTOs.Request;
using LMS.BLL.DTOs.Response;
using LMS.DAL.Entities;

namespace LMS.BLL.Interfaces;

public interface IAssessmentService
{
    Task<Status> CreateAssessment(AssessmentRequestDto assessmentRequest);
    Task<IEnumerable<Assessment>> GetAssessments();
    Task<Assessment> GetAssessment(int id);
    Task<bool> DeleteAssessment(int id);
    Task<Status> UpdateAssessment(EditAssessmentDto requestDto);
    Task<IEnumerable<Assessment>> GetEnrolledAssessmentForAStudent(string studentId);
    Task<IEnumerable<Assessment>> GetAllCompletedAssessment();
}
