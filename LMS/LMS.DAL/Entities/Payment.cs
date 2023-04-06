using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.DAL.Entities
{
    public class Payment : BaseEntity
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public string TrxnRef { get; set; }
  
        public bool Status { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
