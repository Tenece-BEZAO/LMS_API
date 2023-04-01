namespace LMS.DAL.Entities;

public class Assessment : BaseEntity
{
    public string Title { get; set; }
    public string AssessmentType { get; set; }
    public decimal Score { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; }
    public int CourseId { get; set; }
    public Course CourseFor { get; set; }

    //public virtual ICollection<Course> CourseList { get; set; }
    //public virtual ICollection<Student> StudentList { get; set; }
}
