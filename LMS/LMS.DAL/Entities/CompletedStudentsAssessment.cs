namespace LMS.DAL.Entities;

public class CompletedStudentsAssessment
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }

    public Assessment Assessment { get; set; }
    public Student Student { get; set; }
}
