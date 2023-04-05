using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entities
{
    public class CoursePayment : BaseEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }
        public int Amount { get; set; }
        public string TransactionRef { get; set; }

        public bool Status { get; set; }

       // [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

       // [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
