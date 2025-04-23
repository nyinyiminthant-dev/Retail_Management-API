using BAL.IServices;
using MODEL.DTOs;
using MODEL.Entities;
using REPOSITORY.UnitOfWork;

namespace BAL.Services;

public class CategoryService : ICategoryService
{
    
    private readonly IUnitOfWork _unitOfWork;
    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    
    public async Task<CategoryListResponseDTO> GetAllCategories()
    {
        CategoryListResponseDTO model = new CategoryListResponseDTO();
        
        var categories = await _unitOfWork.Category.GetAll();

        if (categories is null)
        {
            model.IsSuccess = false;
            model.Message = "No categories found.";
            model.Data = null;
            return model;
        }
        
        model.IsSuccess = true;
        model.Message = "Categories retrieved successfully.";
        model.Data = categories.ToList();
        
        return model;
    }

    public async Task<CategoryResponseDTO> GetCategoryById(int id)
    {
        
        CategoryResponseDTO model = new CategoryResponseDTO();
        var category = await _unitOfWork.Category.GetByIdAsync(id);
        
        if (category is null)
        {
            model.IsSuccess = false;
            model.Message = "Category not found.";
            model.Data = null;
            return model;
        }
        
        model.IsSuccess = true;
        model.Message = "Category retrieved successfully.";
        model.Data = category;
        
        return model;
    }

    public async Task<CategoryResponseDTO> AddCategory(CategoroyRequestDTO requestDto)
    {
       CategoryResponseDTO model = new CategoryResponseDTO();

       var item = new Category
       {
           Category_Name = requestDto.Category_Name,
       };

       _unitOfWork.Category.Add(item);
       var result =  await _unitOfWork.SaveAsync();
       
       string message = result > 0 ? "Category created successfully." : "Failed";
       
       model.IsSuccess = result > 0;
       model.Message = message;
       model.Data = item;
       
       return model;
       
    }

    public async Task<CategoryResponseDTO> UpdateCategory(int id, CategoroyRequestDTO requestDto)
    {
        CategoryResponseDTO model = new CategoryResponseDTO();
        
        var category = await _unitOfWork.Category.GetByIdAsync(id);

        if (category is null)
        {
            model.IsSuccess = false;
            model.Message = "Category not found.";
            model.Data = null;
            return model;
        }

        if (requestDto.Category_Name is not "string")
        {
            category.Category_Name = requestDto.Category_Name;
        }
        
        _unitOfWork.Category.Update(category);
        var result =  await _unitOfWork.SaveAsync();
        
        string message = result > 0 ? "Category updated successfully." : "Failed";
        
        model.IsSuccess = result > 0;
        model.Message = message;
        model.Data = category;
        
        return model;


    }

    public async Task<CategoryResponseDTO> DeleteCategory(int id)
    {
        
        CategoryResponseDTO model = new CategoryResponseDTO();
        var category = await _unitOfWork.Category.GetByIdAsync(id);
        if (category is null)
        {
            model.IsSuccess = false;
            model.Message = "Category not found.";
            model.Data = null;
            return model;
        }
        
        _unitOfWork.Category.Delete(category);
        var result =  await _unitOfWork.SaveAsync();
        
        string message = result > 0 ? "Category deleted successfully." : "Failed";
        
        model.IsSuccess = result > 0;
        model.Message = message;
        model.Data = category;
        
        return model;
    }
}