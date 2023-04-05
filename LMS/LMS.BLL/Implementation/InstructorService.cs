using LMS.BLL.DTOs.Response;
using LMS.BLL.Exceptions;
using LMS.BLL.Interfaces;
using LMS.DAL.Entities;
using LMS.DAL.Entities.identityEntities;
using LMS.DAL.Repository;
using Microsoft.AspNetCore.Identity;

namespace LMS.BLL.Implementation;

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

    public async Task<InstructorDTO> CreateInstructor(InstructorDTO instructor)
    {
        var user = await _userManager.FindByIdAsync(instructor.UserId);
        if (user != null)
        {
            Instructor newInstructor = new Instructor()
            {
                UserId = instructor.UserId,
                Fullname = instructor.FullName,
                CreatedBy = instructor.FullName
            };

            var createdInstructor = await _repository.AddAsync(newInstructor);
            if (createdInstructor != null)
            {
                return instructor;
            }
        }

        throw new NotFoundException($"User with Id {instructor.UserId} was not found");
    }

    public Task<bool> DeleteInstructor(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task<InstructorDTO> EditInstructor(InstructorDTO instructor)
    {
        throw new System.NotImplementedException();
    }

    public Task<IEnumerable<InstructorDTO>> GetAllInstructors()
    {
        throw new System.NotImplementedException();
    }

    public Task<InstructorDTO> GetInstructorById(int instructorId)
    {
        throw new System.NotImplementedException();
    }
}
