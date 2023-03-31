using LMS.BLL.DTOs.Request;
using LMS.BLL.DTOs.Response;
using LMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Interfaces
{
    public interface IInstructorService
    {
        Task<InstructorDTO> CreateInstructor(InstructorDTO instructor);
        Task<InstructorDTO> EditInstructor(InstructorDTO instructor);
        Task<InstructorDTO> GetInstructorById(int instructorId);

        Task<IEnumerable<InstructorDTO>> GetAllInstructors();
        Task<bool> DeleteInstructor(int id);
    }
}
