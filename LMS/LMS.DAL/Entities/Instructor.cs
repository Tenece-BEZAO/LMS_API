using LMS.DAL.Entities.identityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entities
{
    public class Instructor : BaseEntity
    {
        public string UserId { get; set; }
        public ClaimsPrincipal User { get; set; }
        public string Fullname { get; set; }
        public virtual ICollection<Course> CreatedCourses { get; set; }
    }
}