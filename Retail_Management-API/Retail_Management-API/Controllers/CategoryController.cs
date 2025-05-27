using Asp.Versioning;
using BAL.IServices;
using Microsoft.AspNetCore.Mvc;
using MODEL.ApplicationConfig;
using MODEL.DTOs;

namespace Retail_Management_API.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1.0")]

public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost("Add")]
    public async Task<IActionResult> AddCategory(CategoroyRequestDTO requestDto)
    {
        try
        {
            var response = await _categoryService.AddCategory(requestDto);
            return Ok(new ResponseModel { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        try
        {
            var response = await _categoryService.GetAllCategories();
            return Ok(new ResponseModel { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        try
        {
            var response = await _categoryService.GetCategoryById(id);
            return Ok(new ResponseModel
                { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
        }
    }
    
    [HttpDelete("Delete")]
    
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            var response = await _categoryService.DeleteCategory(id);
            return Ok(new ResponseModel
                { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
        }
    }
    
    [HttpPatch ("Update/{id}")]
    
    public async Task<IActionResult> UpdateCategory(int id, CategoroyRequestDTO requestDto)
    {
        try
        {
            var response = await _categoryService.UpdateCategory(id, requestDto);
            return Ok(new ResponseModel
                { Message = response.Message, Status = APIStatus.Successful, Data = response.Data });
        } catch (Exception ex)
        {
            return BadRequest(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
        }
    }
    
    
    
   
}