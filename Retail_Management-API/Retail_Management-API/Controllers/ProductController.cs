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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var response = await _productService.GetAllProducts();
                return Ok(new ResponseModel { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var response = await _productService.GetProductById(id);
                return Ok(new ResponseModel { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Add")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequestDTO request)
        {
            try
            {
                var response = await _productService.CreateProduct(request);
                return Ok(new ResponseModel { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPatch("Update/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequestDTO request)
        {
            try
            {
                var response = await _productService.UpdateProduct(id, request);
                return Ok(new ResponseModel { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var response = await _productService.DeleteProduct(id);
                return Ok(new ResponseModel { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }
    }
}
