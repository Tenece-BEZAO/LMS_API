namespace LMS.DAL.Entities;

public class CompletedStudentsCourses : BaseEntity
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }

    public Course Course { get; set; }
    public Student Student { get; set; }

    //    public virtual ICollection<Course> CourseList { get; set; }
    //  public virtual ICollection<Student> StudentList { get; set; }
}
