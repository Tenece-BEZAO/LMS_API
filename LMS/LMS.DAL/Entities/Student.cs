using LMS.DAL.Entities.identityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entities
{
    public class Student  : BaseEntity
    {
        public string UserId { get; set; }
        public  AppUser User { get; set; }
        public string FullName { get; set; }
       
        public string Country { get; set; }
        public string State { get; set; }
        public int CourseId { get; set; }
        // public Course Courses { get; set; }

        [NotMapped]
        public virtual ICollection<Course> CompletedCourses { get; set; }
        public virtual ICollection<Course> EnrolledCourses { get; set; }
    }
}
