namespace LMS.BLL.DTOs.Request;

public class AssessmentRequestDto
{
    public string Title { get; set; }
    public string AssessmentType { get; set; }
    public decimal Score { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
}
