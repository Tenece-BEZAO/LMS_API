using LMS.BLL.DTOs.Request;
using LMS.BLL.DTOs.Response;
using LMS.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using LMS.BLL.Infrastructure;
using LMS.DAL.Entities.identityEntities;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Authorization")]
    public class AuthenticationContoller : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IEmailService _emailService;

        public AuthenticationContoller(IAuthenticationService authService, IEmailService emailService )
        {
            _authService = authService;
            _emailService = emailService;
        }

        [HttpPost("Test-Email", Name ="Test Email")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Test Emails")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Email Sent response",
            Type = typeof(AuthenticationResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "User with provided email already exists",
            Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to create user",
            Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us",
            Type = typeof(ErrorResponse))]
        public async Task<IActionResult> TestEmail (string message)
        {
            var response = await _emailService.sendEmail(new Message(new string[] { "Ebube" }, "TestEmail", $"Hey {message}, Email Works"));
            if (response)
            {
                return Ok("Email Sent");
            }

            return BadRequest();

        }

        [AllowAnonymous]
        [HttpPost("", Name = "Create-New-User")]
        [SwaggerOperation(Summary = "Creates user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "UserId of created user",
            Type = typeof(AuthenticationResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "User with provided email already exists",
            Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to create user",
            Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us",
            Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CreateUser(UserRegistrationRequest request)
        {
           // string response = await _authService.CreateUser(request);
            return Ok();
        }


        [AllowAnonymous]
        
        [HttpGet("GetUsers", Name = "Get-All-Users")]
        [SwaggerOperation(Summary = "Gets All Users")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "List of all Identity User", Type = typeof(IEnumerable<AppUser>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "User with provided email already exists", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to create user", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _authService.GetAllUsers();
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("login", Name = "Login")]
        [SwaggerOperation(Summary = "Authenticates user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "returns user Id",
            Type = typeof(AuthenticationResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid username or password",
            Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us",
            Type = typeof(ErrorResponse))]
        public async Task<ActionResult<AuthenticationResponse>> Login(LoginRequest request)
        {
            AuthenticationResponse response = await _authService.UserLogin(request);
            return Ok(response);
        }
    }
}
