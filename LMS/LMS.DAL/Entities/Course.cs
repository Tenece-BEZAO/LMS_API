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


        // lenth = 50 - 100, notNull
        public string Title { get; set; }

        // lenth = 100 - 1000, null
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

      //  [ForeignKey("EnrolledStudentsId")]
        public int StudentId { get; set; }

        //[ForeignKey("CompletedStudentsId")]
        //public int CompletedStudentId { get; set; }
        public virtual Student Students { get; set; }


        [NotMapped]
        public virtual IEnumerable<Student> EnrolledStudents { get; set; }


        /// <summary>
        ///  [NotMapped]
        /// </summary>
        [NotMapped]
        public virtual IEnumerable<Student> CompletedStudents { get; set; }



    }

   public  enum CourseType
    {
        Public  = 1,
        Private
    }
}
