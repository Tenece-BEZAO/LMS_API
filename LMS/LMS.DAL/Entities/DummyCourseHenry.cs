using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entities
{
    public class DummyCourseHenry : DummyBaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string CourseCode { get; set; }
        public string  CourseVideoUrl { get; set; }
        public string CoursePdfsUrl { get; set; }


    }
}
