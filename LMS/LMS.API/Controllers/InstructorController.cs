using LMS.BLL.DTOs.Request;
using LMS.BLL.DTOs.Response;
using LMS.BLL.Implementation;
using LMS.BLL.Interfaces;
using LMS.DAL;
using LMS.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class InstructorController : ControllerBase
    {   
            private readonly IInstructorService _instructorService;

            public InstructorController(IInstructorService instructorService)
            {
                _instructorService = instructorService;
            }

            [HttpGet]
            [Route("get-all-instructors")]
            public async Task<IActionResult> GetAllInstructors()
            {
                var result = await _instructorService.GetAllInstructors();
                if (result == null)
                    return BadRequest();

                return Ok(result);
            }

            [HttpGet]
            [Route("get-instructor-by-id")]
            public async Task<IActionResult> GetInstructorById(int id)
            {
                var result = await _instructorService.GetInstructorById(id);
                if (result == null)
                    return BadRequest();

                return Ok(result);
            }

            [HttpPost]
            [Route("create-instructor")]
            public async Task<IActionResult> CreateInstructor([FromBody] InstructorDTO instructor)
            {
                var result = await _instructorService.CreateInstructor(instructor);
                if (result == null)
                    return BadRequest();


                return Ok(result);
            }

            [HttpDelete]
            [Route("delete-instructor")]
            public async Task<IActionResult> DeleteInstructor(int id)
        {
                var result = await _instructorService.DeleteInstructor(id);
                if (!result)
                {
                    return BadRequest();
                }

                return Ok("Instructor was deleted successfully");
            }

            [HttpPut]
            [Route("update-Instructor")]
            public async Task<IActionResult> UpdateInstructor([FromBody] InstructorDTO instructor)
            {
            InstructorDTO instructorResult = await _instructorService.EditInstructor(instructor);
                if (instructorResult == null)
                {
                    return BadRequest("something went wrong");
                }
                return Ok(instructorResult);
            }
        }

                                                 
}
