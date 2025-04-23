using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using MODEL.DTOs;

namespace BAL.IServices;

public interface IProductService
{
    Task<ProductListResponseDTO> GetAllProducts();
    Task<ProductResponseDTO> GetProductById(int id);
    Task<ProductResponseDTO> CreateProduct(ProductRequestDTO requestDTO, IFormFile? photo);
    Task<ProductResponseDTO> UpdateProduct(int id,ProductRequestDTO requestDTO);
    Task<ProductResponseDTO> DeleteProduct(int id);

    Task<ProductResponseDTO> IncreaseQuantity(int id, ProductRequestDTO requestDTO);
  
}
