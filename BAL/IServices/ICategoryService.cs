using MODEL.DTOs;

namespace BAL.IServices;

public interface ICategoryService
{
    
    Task<CategoryListResponseDTO> GetAllCategories();
    
    Task<CategoryResponseDTO> GetCategoryById(int id);

    Task<CategoryResponseDTO> AddCategory(CategoroyRequestDTO requestDto);
    
    Task<CategoryResponseDTO> UpdateCategory(int id, CategoroyRequestDTO requestDto);
    
    Task<CategoryResponseDTO> DeleteCategory(int id);
    
    
}