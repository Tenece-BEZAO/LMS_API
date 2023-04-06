using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.DTOs.Request
{
    public class PaymentRequest
    {
        public int StudentId { get; set; }

        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Amount { get; set; }
    }
}
