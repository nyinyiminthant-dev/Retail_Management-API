using Asp.Versioning;
using BAL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL.ApplicationConfig;

namespace Retail_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("2.0")]
    [ApiController]
  
    public class ConfirmController : ControllerBase
    {
        private readonly IConfirmOrderService    _orderService;

        public ConfirmController(IConfirmOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Confirm")]

        public async Task<IActionResult> ConfirmOrder(int id)
        {
            try
            {
                var response = await _orderService.ConfirmOrder(id);
                return Ok(new ResponseModel { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }
    }
}
