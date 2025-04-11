using BAL.IServices;
using MODEL.DTOs;
using MODEL.Entities;
using REPOSITORY.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ProductResponseDTO> CreateProduct(ProductRequestDTO requestDTO)
        {
           var prodcut = new Product
           {
               Name = requestDTO.Name,
               Stock = requestDTO.Stock,
               Price = requestDTO.Price,
               Profit = requestDTO.Profit
           };
            await _unitOfWork.Product.Add(prodcut);
          int result =   await _unitOfWork.SaveAsync();

            return new ProductResponseDTO
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Product created successfully." : "Failed to create product.",
                Data = prodcut
            };

        }

        public async Task<ProductResponseDTO> DeleteProduct(int id)
        {
           
            var producId = await _unitOfWork.Product.GetById(id);

            if (producId == null)
            {
                return new ProductResponseDTO
                {
                    IsSuccess = false,
                    Message = "Product not found.",
                    Data = null
                };
            }

            _unitOfWork.Product.Delete(producId);
            int result = await _unitOfWork.SaveAsync();

            return new ProductResponseDTO
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Product deleted successfully." : "Failed to delete product.",
                Data = null
            };



        }

        public async Task<ProductListResponseDTO> GetAllProducts()
        {
            ProductListResponseDTO model = new ProductListResponseDTO();
            var products = await _unitOfWork.Product.GetAll();

            if(products is null)
            {
                model.Message = "No Found";
                model.IsSuccess = false;
                model.Data = null;
            }

            model.Message = "Success";
            model.IsSuccess = true;
            model.Data = products.ToList();

            return model;

          
        }

        public async Task<ProductResponseDTO> GetProductById(int id)
        {
           
            var product = await _unitOfWork.Product.GetById(id);

            return new ProductResponseDTO
            {
                IsSuccess = product != null,
                Message = product != null ? "Product retrieved." : "Product not found.",
                Data = product!
            };
        }

        public async Task<ProductResponseDTO> UpdateProduct(int id, ProductRequestDTO requestDTO)
        {
           var productId = await _unitOfWork.Product.GetByIdAsync(id);

            if (productId == null)
            {
                return new ProductResponseDTO
                {
                    IsSuccess = false,
                    Message = "Product not found.",
                    Data = null
                };
            }
            productId.Name = requestDTO.Name;
            productId.Stock = requestDTO.Stock;
            productId.Price = requestDTO.Price;
            productId.Profit = requestDTO.Profit;
            _unitOfWork.Product.Update(productId);
            int result = await _unitOfWork.SaveAsync();
            return new ProductResponseDTO
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Product updated successfully." : "Failed to update product.",
                Data = productId
            };
        }
    }
}
