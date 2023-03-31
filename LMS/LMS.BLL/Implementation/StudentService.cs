using AutoMapper;
using LMS.BLL.DTOs.Request;
using LMS.BLL.DTOs.Response;
using LMS.BLL.Interfaces;
using LMS.DAL.Entities;
using LMS.DAL.Repository;

namespace LMS.BLL.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;
        private readonly IAuthenticationService _authService;
        private readonly IRepository<Student> _studentRepo;


        public StudentService(IUnitOfWork unitOfWork, IMapper autoMapper, IAuthenticationService authService)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
            _authService = authService;
            _studentRepo = _unitOfWork.GetRepository<Student>();
        }

        public async Task<string> CreateStudent(StudentDTO student)
        {
            if (student.UserId == null || student.UserId == "")
            {
                var CreatedUserIdResult = await _authService.CreateUser(new UserRegistrationRequest
                    { Email = student.Email, UserName = student.UserName, Password = student.Password });

                student.UserId = CreatedUserIdResult;
            }

            Student newStudent = new Student
            {
                UserId = student.UserId,
                FullName = student.Firstname + student.LastName,
                Country = student.Country, State = student.State
            };


            var result = _studentRepo.AddAsync(newStudent);
            if (result.IsCompletedSuccessfully)
                return result.Result.Id.ToString();

            throw new NotImplementedException("Student could not be created");
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var result = _studentRepo.DeleteByIdAsync(id);
            if (result.IsCompletedSuccessfully) return true;
            return false;
        }

        public async Task<bool> EditStudent(StudentDTO student)
        {
            Student Updatestudent = new Student
            {
                UserId = student.UserId,
                FullName = student.Firstname + student.LastName,
                Country = student.Country,
                State = student.State
            };
            var result = _studentRepo.UpdateAsync(Updatestudent);

            if (result.IsCompletedSuccessfully) return true;
            return false;
        }

        public async Task<IEnumerable<StudentDTO>> GetAllStudents()
        {
            var allStudents = await _studentRepo.GetAllAsync();
            // var ListOfStudents =  _autoMapper.ProjectTo<IEnumerable<StudentDTO>>(allStudents.ToList());


            return null;
            //return ListOfStudents;
        }

        public Task<StudentDTO> GetStudentById(int student)
        {
            throw new NotImplementedException();
        }

        Task<StudentDTO> IStudentService.EditStudent(StudentDTO student)
        {
            throw new NotImplementedException();
        }
    }
}
