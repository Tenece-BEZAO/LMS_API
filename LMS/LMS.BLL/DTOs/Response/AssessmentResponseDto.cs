using LMS.DAL.Entities;

namespace LMS.BLL.DTOs.Response;

public class AssessmentResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string AssessmentType { get; set; }
    public decimal Score { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public Course CourseFor { get; set; }
    public Student Student { get; set; }
}
