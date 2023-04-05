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
}
