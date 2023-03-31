using LMS.BLL.DTOs.Request;
using LMS.BLL.DTOs.Response;
using LMS.BLL.Infrastructure;
using LMS.BLL.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Route("get-all-app-roles")]
        [AllowAnonymous]
       
        [SwaggerOperation(Summary = "Get roles")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Created Role Success Response Message", Type = typeof(AuthenticationResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Role with ID already Exists", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to create role", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]

        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleService.GetAllRoles();
            return Ok(roles);
        }


        [HttpPost]
        [Route("create-a-role")]
        [AllowAnonymous]
      //  [HttpPost("", Name = "Create-New-Role")]
        [SwaggerOperation(Summary = "Creates role")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "RoleResult of created user", Type = typeof(AuthenticationResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Role already exists", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to create role", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]

        public async Task<IActionResult> CreateRole(string name)
        {
            RoleResult obj = await _roleService.CreateRole(new RoleDto { Name = name});
            if (obj.result == false)
            {
                return BadRequest(obj);
            }



            return StatusCode(201, obj);
        }


        [HttpGet]
        [Route("get-all-users")]
        [AllowAnonymous]
       // [HttpGet("", Name = "Get-All-User")]
        [SwaggerOperation(Summary = "Get All Role")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "UserId of created user", Type = typeof(AuthenticationResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "User with provided email already exists", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to create user", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]

        public async Task<IActionResult> GetAllUsers()
        {
           // var result = await _roleService.GetAllUser();
            return Ok();
        }


        [HttpPost]
        [Route("add-user-to-role")]
        [AllowAnonymous]
      //  [HttpPost("", Name = "Add-User-To-Role")]
        [SwaggerOperation(Summary = "Adds a User to Role")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "User Added to Role Response", Type = typeof(AuthenticationResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "User akready added to Role", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to Add User to Role", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]

        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            var result =  _roleService.AddUserToRole(new AddUserToRoleRequest { Role = roleName, UserName = email });
            if (result.IsCompletedSuccessfully)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet]
        [Route("get-user-roles")]

        public async Task<IActionResult> GetUserRoles(string email)
        {
            var result = await _roleService.GetUserRoles(email);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("remove-user-from-role")]
        public async Task<IActionResult> RemoveUserFromRole(string email, string roleName)
        {
            var result = _roleService.RemoveUserFromRole(new AddUserToRoleRequest { Role = roleName, UserName = email });
            if (result.IsCompletedSuccessfully)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}