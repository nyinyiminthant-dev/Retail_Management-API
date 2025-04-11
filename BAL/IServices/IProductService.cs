using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.DTOs;

namespace BAL.IServices;

public interface IProductService
{
    Task<ProductListResponseDTO> GetAllProducts();
    Task<ProductResponseDTO> GetProductById(int id);
    Task<ProductResponseDTO> CreateProduct(ProductRequestDTO requestDTO);
    Task<ProductResponseDTO> UpdateProduct(int id,ProductRequestDTO requestDTO);
    Task<ProductResponseDTO> DeleteProduct(int id);
  
}
