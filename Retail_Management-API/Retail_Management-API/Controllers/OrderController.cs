using Asp.Versioning;
using BAL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MODEL.ApplicationConfig;
using MODEL.DTOs;
using System;
using System.Threading.Tasks;

namespace Retail_Management_API.Controllers
{
    [ApiController]
   
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var response = await _orderService.GetAllOrders();
                return Ok(new ResponseModel { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }

        }


        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var response = await _orderService.GetOrderById(id);
                return Ok(new ResponseModel { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });

            }
        }

        [Authorize(Roles = "User")]
        [HttpPost("Add")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequestDTO request)
        {
            try
            {
                var response = await _orderService.CreateOrder(request);
                return Ok(new ResponseModel { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }

        }

        [Authorize(Roles = "User")]
        [HttpPatch("Update/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderRequestDTO request)
        {
            try
            {
                var response = await _orderService.UpdateOrder(id, request);

               return Ok(new ResponseModel { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var response = await _orderService.DeleteOrder(id);
                return Ok(new ResponseModel { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }
    }
}
