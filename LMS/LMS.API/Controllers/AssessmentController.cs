using LMS.BLL.DTOs.Request;
using LMS.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AssessmentController : ControllerBase
{
    private readonly IAssessmentService _assessmentService;

    public AssessmentController(IAssessmentService assessmentService)
    {
        _assessmentService = assessmentService;
    }

    [HttpPost("CreateAssessment")]
    public async Task<IActionResult> CreateAssessment(AssessmentRequestDto assessmentRequest)
    {
        var assessment = await _assessmentService.CreateAssessment(assessmentRequest);

        if (assessment is null)
            return BadRequest();

        return Ok(assessment);
    }

    [HttpGet("GetAssessments")]
    public async Task<IActionResult> GetAssessments()
    {
        var assessments = await _assessmentService.GetAssessments();

        if (assessments is null)
            return NotFound();

        return Ok(assessments);
    }

    [HttpGet("GetAssessment")]
    public async Task<IActionResult> GetAssessment(int id)
    {
        var assessment = await _assessmentService.GetAssessment(id);

        if (assessment is null)
            return NotFound();

        return Ok(assessment);
    }

    [HttpPut("UpdateAssessment")]
    public async Task<IActionResult> UpdateAssessment(EditAssessmentDto requestDto)
    {
        var updatedAssessment = await _assessmentService.UpdateAssessment(requestDto);

        if (updatedAssessment is null)
            return BadRequest();

        return Ok(updatedAssessment);
    }

    [HttpDelete("DeleteAssessment")]
    public async Task<IActionResult> DeleteAssessment(int id)
    {
        var assessment = await _assessmentService.DeleteAssessment(id);

        if (!assessment)
            return NotFound();

        return Ok(assessment);
    }
}
