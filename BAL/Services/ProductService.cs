﻿
using AutoMapper;
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
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ProductResponseDTO> CreateProduct(ProductRequestDTO requestDTO)
        {
            //var prodcut = new Product
            //{
            //    Name = requestDTO.Name,
            //    Stock = requestDTO.Stock,
            //    Price = requestDTO.Price,
            //    Profit = requestDTO.Profit,
            //    CreatedDate = DateTime.Now,
            //    UpdateDate = DateTime.Now,
            //    IsOut = "N"

            //};

            var product = _mapper.Map<Product>(requestDTO);
            product.CreatedDate = DateTime.Now;
            product.UpdateDate = DateTime.Now;
            product.IsOut = "N";

            await _unitOfWork.Product.Add(product);
          int result =   await _unitOfWork.SaveAsync();

            return new ProductResponseDTO
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Product created successfully." : "Failed to create product.",
                Data = product
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

                return model;
            }

            model.Message = "Success";
            model.IsSuccess = true;
            model.Data = products.ToList();

            return model;

          
        }

        public async Task<ProductResponseDTO> GetProductById(int id)
        {

            ProductResponseDTO model = new ProductResponseDTO();
           
            var product = await _unitOfWork.Product.GetById(id);

            if(product.IsOut == "Y")
            {
                model.Message = "Out of stock";
                model.IsSuccess = true;
                model.Data = product;
            }


                model.IsSuccess = product != null;
                model.Message = product != null ? "Product retrieved." : "Product not found.";
                model.Data = product!;

            return model;
           
        }

        public async Task<ProductResponseDTO> IncreaseQuantity(int id, ProductRequestDTO requestDTO)
        {
            ProductResponseDTO model = new ProductResponseDTO();

            var product = await _unitOfWork.Product.GetById(id);
            
            if(product is null)
            {
                model.IsSuccess= false;
                model.Message = "No product found with that id.";
                model.Data = null;

                return model;
            }

            if(requestDTO.Stock is not  0)
            {
                product.Stock += requestDTO.Stock;
            }

            _unitOfWork.Product.Update(product);
           var result =   await _unitOfWork.SaveAsync();

            string message = result > 0 ? "Increase stock successful" : "Failed";

            model.IsSuccess = result > 0;
            model.Message = message;
            model.Data = product;

            return model;

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
                    Data = null!
                };
            }
            if (requestDTO.Name is not "string")
            {

                productId.Name = requestDTO.Name;
            }

            if (requestDTO.Stock != 0)
            {
                productId.Stock = requestDTO.Stock;

            }

            if (requestDTO.Price != 0 && requestDTO.Price > 0)
            {

                productId.Price = requestDTO.Price;
            }

            if (requestDTO.Profit != 0 && requestDTO.Profit > 0)
            {
                productId.Profit = requestDTO.Profit;
            }


            if (productId.Stock > 0)
            {
                productId.IsOut = "N";
            }
            else if (productId.Stock < 0)
            {
                productId.IsOut = "Y";
                productId.Stock = 0;
            }

            _mapper.Map(requestDTO, productId);

            productId.UpdateDate = DateTime.Now;
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
