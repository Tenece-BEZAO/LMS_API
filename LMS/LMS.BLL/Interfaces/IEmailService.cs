using LMS.BLL.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Interfaces
{
    public interface IEmailService
    {
        Task<bool> sendEmail(Message message);
    }
}
