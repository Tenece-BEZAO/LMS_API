using LMS.DAL.Entities;
using PayStack.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.DTOs.Response
{
    public class EnrolledCourse
    {
        public EnrolledStudentsCourses enrolledStudentsCourses { get; set; }
        public TransactionInitializeResponse transactionInitializeResponse { get; set; }
    }
}
