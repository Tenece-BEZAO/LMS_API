using LMS.BLL.DTOs.Request;
using LMS.BLL.Interfaces;
using LMS.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        [Route("get-all-courses")]
        public async Task<IActionResult> GetAllCourses()
        {
            var result = await _courseService.GetAllCourse();
            if (result == null)
                return BadRequest("Uable to complete request");

            return Ok(result);
        }

        [HttpGet]
        [Route("get-course-by-id")]
        public async Task<IActionResult> GetAllCourseById(int id)
        {
            var result = await _courseService.GetCourseById(id);
            if (result == null)
                return BadRequest("Uable to complete request");

            return Ok(result);
        }

        [HttpPost]
        [Route("create-course")]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto course)
        {
            if (ModelState.IsValid)
            {
                var result = await _courseService.CreateCourse(course);
                if (result == null)
                    return BadRequest("Uable to complete request");


                return Ok(result);
            }
            return BadRequest("Unable to complete request");
        }

        [HttpDelete]
        [Route("delete-course")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var result = await _courseService.DeleteCourse(id);
            if (!result)
            {
                return BadRequest("Uable to complete request");
            }

            return Ok("Course was deleted successfully");
        }

        [HttpPut]
        [Route("update-course")]
        public async Task<IActionResult> UpdateCourse([FromBody] EditCourseDto editCourse)
        {
            if (ModelState.IsValid)
            {
                Course course = await _courseService.EditCourse(editCourse);
                if (course == null)
                {
                    return BadRequest("something went wrong");
                }
                return Ok(course);
            }
            return BadRequest("Unable to complete request");
        }

        [HttpPost]
        [Route("student-enroll-for-course")]
        public async Task<IActionResult> EnrollCourse([FromBody] CourseEnrollDto courseEnrollDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _courseService.EnrollForACourse(courseEnrollDto);
                if (result == null)
                    return BadRequest("unale to complete request");

                return Ok(result);

            }
            return BadRequest(ModelState);
        }



    }
}
