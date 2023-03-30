using LMS.BLL.DTOs.Response;
using LMS.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        
            private readonly IStudentService _studentService;

            public StudentController(IStudentService studentService)
            {
                 _studentService = studentService;
            }

            [HttpGet]
            [Route("get-all-students")]
            public async Task<IActionResult> GetAllStudents()
            {
                var result = await _studentService.GetAllStudents();
                if (result == null)
                    return BadRequest();

                return Ok(result);
            }

            [HttpGet]
            [Route("get-student-by-id")]
            public async Task<IActionResult> GetStudentById(int id)
            {
                var result = await _studentService.GetStudentById(id);
                if (result == null)
                    return BadRequest();

                return Ok(result);
            }

            [HttpPost]
            [Route("create-student")]
            public async Task<IActionResult> CreateInstructor([FromBody] StudentDTO student)
            {
                var result = await _studentService.CreateStudent(student);
                if (result == null)
                    return BadRequest();


                return Ok(result);
            }

            [HttpDelete]
            [Route("delete-student")]
            public async Task<IActionResult> DeleteInstructor(int id)
            {
                var result = await _studentService.DeleteStudent(id);
                if (!result)
                {
                    return BadRequest();
                }

                return Ok("Student was deleted successfully");
            }

            [HttpPut]
            [Route("update-Instructor")]
            public async Task<IActionResult> UpdateInstructor([FromBody] StudentDTO student)
            {
               StudentDTO studentDto = await _studentService.EditStudent(student);
                if (studentDto == null)
                {
                    return BadRequest("something went wrong");
                }
                return Ok(studentDto);
            }
        }


    }

