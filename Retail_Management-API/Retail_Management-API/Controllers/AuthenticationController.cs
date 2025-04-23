using Asp.Versioning;
using BAL.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL.ApplicationConfig;
using MODEL.DTOs;
using REPOSITORY.UnitOfWork;

namespace Retail_Management_API.Controllers
{
    [Route("api/Login")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationController(IAuthenticationService authenticationService, IUnitOfWork unitOfWork)
        {
            _authenticationService = authenticationService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("LoginWeb")]
        public async Task<IActionResult> LoginWeb(UserLoginDTO request)
        {
            try
            {
                var returndata = await _authenticationService.LoginWeb(request);
                if (!returndata.EmailStatus)
                {
                    return Ok(new ResponseModel { Message = "Your email is invalid!", Status = APIStatus.Error });
                }
                else if (!returndata.PasswordStatus)
                {
                    return Ok(new ResponseModel { Message = "Your password is invalid!", Status = APIStatus.Error });
                }
                return Ok(new ResponseModel { Message = Messages.Successfully, Status = APIStatus.Successful, Data = returndata });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

    }
}
