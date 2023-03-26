using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entities
{
    public class Assessment  : BaseEntity
    {
        public string Title { get; set; }

        public string AssessmentType { get; set; }
        public decimal AssessmentScore { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int CourseId { get; set; }
        public virtual Course CourseFor { get; set; }


    }

    public enum AssessmentType
    {
        Quiz = 1,
        Test, 
        Exam
    }
}
