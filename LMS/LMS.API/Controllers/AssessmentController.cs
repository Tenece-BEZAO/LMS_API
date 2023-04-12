using LMS.BLL.DTOs.Request;
using LMS.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssessmentController : ControllerBase
{
    private readonly IAssessmentService _assessmentService;

    public AssessmentController(IAssessmentService assessmentService)
    {
        _assessmentService = assessmentService;
    }

    [SwaggerOperation(Summary = "Create Assessment")]
    [HttpPost]
    public async Task<IActionResult> CreateAssessment(AssessmentRequestDto assessmentRequest)
    {
        var assessment = await _assessmentService.CreateAssessment(assessmentRequest);

        if (assessment is null)
            return BadRequest();

        return Ok(assessment);
    }

    [SwaggerOperation(Summary = "Get All Assessments")]
    [HttpGet]
    public async Task<IActionResult> GetAssessments()
    {
        var assessments = await _assessmentService.GetAssessments();

        if (assessments is null)
            return NotFound();

        return Ok(assessments);
    }

    [SwaggerOperation(Summary = "Get An Assessment")]
    [HttpGet("id")]
    public async Task<IActionResult> GetAssessment(int id)
    {
        var assessment = await _assessmentService.GetAssessment(id);

        if (assessment is null)
            return NotFound();

        return Ok(assessment);
    }

    [SwaggerOperation(Summary = "Update An Assessment")]
    [HttpPut("id")]
    public async Task<IActionResult> UpdateAssessment(EditAssessmentDto requestDto)
    {
        var updatedAssessment = await _assessmentService.UpdateAssessment(requestDto);

        if (updatedAssessment is null)
            return BadRequest();

        return Ok(updatedAssessment);
    }

    [SwaggerOperation(Summary = "Delete An Assessment")]
    [HttpDelete("id")]
    public async Task<IActionResult> DeleteAssessment(int id)
    {
        var assessment = await _assessmentService.DeleteAssessment(id);

        if (!assessment)
            return NotFound();

        return Ok(assessment);
    }

    [SwaggerOperation(Summary = "Get Enrolled Assessments For A Student")]
    [HttpGet("GetEnrolledAssessmentforAStudent")]
    public async Task<IActionResult> GetEnrolledAssessmentforAStudent(string studentId)
    {
        var enrolledAssessments = await _assessmentService.GetEnrolledAssessmentForAStudent(studentId);

        if (enrolledAssessments is null)
            return NotFound();

        return Ok(enrolledAssessments);
    }

    [SwaggerOperation(Summary = "Gets All Completed Assessment")]
    [HttpGet("GetAllCompletedAssessment")]
    public async Task<IActionResult> GetAllCompletedAssessment()
    {
        var isCompleted = await _assessmentService.GetAllCompletedAssessment();

        if (isCompleted is null)
            return NotFound();

        return Ok(isCompleted);
    }
}
