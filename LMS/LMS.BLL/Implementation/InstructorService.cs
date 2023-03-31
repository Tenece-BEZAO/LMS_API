using LMS.BLL.DTOs.Response;
using LMS.BLL.Interfaces;
using LMS.DAL.Entities;
using LMS.DAL.Entities.identityEntities;
using Microsoft.AspNetCore.Identity;
using LMS.DAL.Repository;

namespace LMS.BLL.Implementation
{
    public class InstructorService : IInstructorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<Instructor> _repository;


        public InstructorService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _repository = _unitOfWork.GetRepository<Instructor>();
        }

        public Task<InstructorDTO> CreateInstructor(InstructorDTO instructor)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteInstructor(int id)
        {
            throw new NotImplementedException();
        }

        public Task<InstructorDTO> EditInstructor(InstructorDTO instructor)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InstructorDTO>> GetAllInstructors()
        {
            throw new NotImplementedException();
        }

        public Task<InstructorDTO> GetInstructorById(int instructorId)
        {
            throw new NotImplementedException();
        }
    }
}
