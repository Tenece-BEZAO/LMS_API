using LMS.BLL.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDTO> CreateStudent(StudentDTO student);
        Task<StudentDTO> EditStudent(StudentDTO student);
        Task<StudentDTO> GetStudentById(int student);

        Task<IEnumerable<StudentDTO>> GetAllStudents();
        Task<bool> DeleteStudent(int id);
    }
}
