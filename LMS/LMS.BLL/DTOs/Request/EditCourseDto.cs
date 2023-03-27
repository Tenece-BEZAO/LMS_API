using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.DTOs.Request
{
    public class EditCourseDto
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Detail { get; set; }

        public string HeaderImageUrl { get; set; }
        public decimal? Price { get; set; }

        public string VideoResourceUrl { get; set; }

        public string TextResourceUrl { get; set; }
        public string AdditionalResourcesUrl { get; set; }
        public bool IsActive { get; set; }

        public string CourseType { get; set; }
        public int InstructorId { get; set; }
    }
}
