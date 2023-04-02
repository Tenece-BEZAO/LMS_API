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


      //  public virtual Assessment Assessment { get; set; }
        public virtual ICollection<Assessment> Assessments { get; set; }


        public virtual ICollection<EnrolledStudentsCourses> EnrolledCourses { get; set; }
        public virtual ICollection<CompletedStudentsCourses> CompletedCourses { get; set; }

    }
}
