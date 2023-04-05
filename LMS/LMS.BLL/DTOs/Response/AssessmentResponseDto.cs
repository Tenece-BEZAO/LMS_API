using LMS.DAL.Entities;

namespace LMS.BLL.DTOs.Response;

public class AssessmentResponseDto
{
    public string Title { get; set; }
    public string AssessmentType { get; set; }
    public decimal Score { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; }
    public int InstructorId { get; set; }
    public int CourseId { get; set; }
}
