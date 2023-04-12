using LMS.DAL.Entities;

namespace LMS.BLL.DTOs.Request;

public class CompletedStudentsAssessmentDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }

    public Assessment Assessment { get; set; }
    public int AssessmentId { get; set; }
    public Student Student { get; set; }
}
