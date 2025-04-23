using Asp.Versioning;
using BAL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL.ApplicationConfig;
using MODEL.DTOs;

namespace Retail_Management_API.Controllers;

[Route("api/[controller]")]

[ApiController]
[ApiVersion("1.0")]
public class UserController : ControllerBase
{

    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = "Admin")]

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var response = await _userService.GetAllUsers();
            return Ok(new ResponseModel { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
        }
    }


    [Authorize(Roles = "Admin")]

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var response = await _userService.GetUserById(id);
            return Ok(new ResponseModel { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
        }
    }

    [HttpPost("Register")]
    public async Task<IActionResult> CreateUser([FromBody] UserRequestDTO request)
    {
        try
        {
            var response = await _userService.CreateUser(request);
            return Ok(new ResponseModel { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
        }
    }

    [Authorize(Roles = "Admin,User")]
    [HttpPatch("Update/{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserRequestDTO request)
    {
        try
        {
            var response = await _userService.UpdateUser(id, request);
            return Ok(new ResponseModel { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
        }
    }

    [HttpPost("VerifyEmail")]

    public async Task<IActionResult> VerifyEmail(string email, string otp)
    {
        try
        {
            var response = await _userService.VerifyEmail(email, otp);
            return Ok(new ResponseModel { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpPatch("Ban/{id}")]
    public async Task<IActionResult> Ban(int id)
    {
        try
        {
            var response = await _userService.Ban(id);
            return Ok(new ResponseModel { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpGet("GetByName/{name}")]
    public async Task<IActionResult> GetUserByName(string name)
    {
        try
        {
            var response = await _userService.GetUserByName(name);
            return Ok(new ResponseModel { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
        }
    }
}
