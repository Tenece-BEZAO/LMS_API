using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entities
{
    public class EnrolledStudentsCourses : BaseEntity
    {
        
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public Course Courses { get; set; }
        public Student Students { get; set; }
    }
}
