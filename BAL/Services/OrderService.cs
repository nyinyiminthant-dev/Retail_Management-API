using BAL.IServices;
using MODEL.DTOs;
using MODEL.Entities;
using REPOSITORY.UnitOfWork;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderResponseDTO> CreateOrder(OrderRequestDTO requestDTO)
        {
            var product = await _unitOfWork.Product.GetByIdAsync(requestDTO.ProductId);
            if (product == null)
            {
                return new OrderResponseDTO
                {
                    IsSuccess = false,
                    Message = "Product not found."
                };
            }

            var order = new Order
            {
                ProductId = product.ProductId,
                ProductName = product.Name,
                Quantity = requestDTO.Quantity,
                Price = product.Price * requestDTO.Quantity,
                Profit = product.Profit * requestDTO.Quantity,
                Status = requestDTO.Status
            };

            await _unitOfWork.Order.Add(order);
            int result = await _unitOfWork.SaveAsync();

            return new OrderResponseDTO
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Order created successfully." : "Failed to create order.",
                Data = order
            };
        }

        public async Task<OrderResponseDTO> DeleteOrder(int id)
        {
            var order = await _unitOfWork.Order.GetByIdAsync(id);
            if (order == null)
            {
                return new OrderResponseDTO
                {
                    IsSuccess = false,
                    Message = "Order not found."
                };
            }

            _unitOfWork.Order.Delete(order);
            int result = await _unitOfWork.SaveAsync();

            return new OrderResponseDTO
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Order deleted successfully." : "Failed to delete order.",
                Data = order
            };
        }

        public async Task<OrderResponseDTO> GetAllOrders()
        {
            var orders = await _unitOfWork.Order.GetAll();
            var firstOrder = orders.FirstOrDefault();

            return new OrderResponseDTO
            {
                IsSuccess = firstOrder != null,
                Message = firstOrder != null ? "Orders retrieved." : "No orders found.",
                Data = firstOrder
            };
        }

        public async Task<OrderResponseDTO> GetOrderById(int id)
        {
            var order = await _unitOfWork.Order.GetByIdAsync(id);

            return new OrderResponseDTO
            {
                IsSuccess = order != null,
                Message = order != null ? "Order retrieved." : "Order not found.",
                Data = order!
            };
        }

        public async Task<OrderResponseDTO> UpdateOrder(int id, OrderRequestDTO requestDTO)
        {
            var order = await _unitOfWork.Order.GetByIdAsync(id);
            if (order == null)
            {
                return new OrderResponseDTO
                {
                    IsSuccess = false,
                    Message = "Order not found."
                };
            }

            var product = await _unitOfWork.Product.GetByIdAsync(id);
            if (product == null)
            {
                return new OrderResponseDTO
                {
                    IsSuccess = false,
                    Message = "Product not found."
                };
            }

            order.ProductId = product.ProductId;
            order.ProductName = product.Name;
            order.Quantity = requestDTO.Quantity;
            order.Price = product.Price * requestDTO.Quantity;
            order.Profit = product.Profit * requestDTO.Quantity;
            order.Status = requestDTO.Status;

            _unitOfWork.Order.Update(order);
            int result = await _unitOfWork.SaveAsync();

            return new OrderResponseDTO
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Order updated successfully." : "Failed to update order.",
                Data = order
            };
        }
    }
}
