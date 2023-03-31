using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entities
{
    public class Course : BaseEntity
    {


        
        public string Title { get; set; }

        
        public string Detail { get; set; }

        public string? HeaderImageUrl { get; set; }
        public decimal? Price { get; set; }
                                           
        public string? VideoResourceUrl { get; set; }
        public string? TextResourceUrl { get; set; }
        public string? AdditionalResourcesUrl  { get; set; }
        public string CourseType { get; set; }
        public int InstructorId  { get; set; }

        public virtual Instructor CourseOwner { get; set; }
        public bool IsActive { get; set; }

      //  public virtual Assessment Assessment { get; set; }

        public virtual ICollection<Assessment> Assessments { get; set; }

        public virtual ICollection<EnrolledStudentsCourses> EnrolledStudents { get; set; }

        public virtual ICollection<CompletedStudentsCourses> StudentsCompleted { get; set; }



    }

   public  enum CourseType
    {
        Public  = 1,
        Private
    }
}
