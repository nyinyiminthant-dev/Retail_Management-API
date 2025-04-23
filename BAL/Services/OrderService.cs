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

        public async Task<OrderResponseDTO> InsertOrder(OrderRequestDTO requestDTO)
        {
            OrderResponseDTO model = new OrderResponseDTO();

            var product = await _unitOfWork.Product.GetByIdAsync(requestDTO.ProductId);
            var user = await _unitOfWork.User.GetByIdAsync(requestDTO.UserId);
            if (product == null)
            {
                return new OrderResponseDTO
                {
                    IsSuccess = false,
                    Message = "Product not found."
                };
            }

            if(product.IsOut == "Y")
            {
                model.IsSuccess = true;
                model.Message = "Product is out of stock";
                model.Data = null;
                return model;
            }

            var order = new Order
            {
                ProductId = product.ProductId,
                ProductName = product.Name,
                Quantity = requestDTO.Quantity,
                Price = product.Price,
                Profit = product.Profit,
                IsOrder = "pending",
                SaleDate = DateTime.Now,
                TotalPrice = product.Price * requestDTO.Quantity,
                TotalProfit = product.Profit * requestDTO.Quantity,
                
                UserId = user.UserID,
            };

          

            if (product.Stock < requestDTO.Quantity)
            {
                model.IsSuccess = false;
                model.Message = "Please decreas the quantity";
                return model;
            }

            product.Stock -= order.Quantity;

            if (product.Stock == 0)
            {
                product.IsOut = "Y";
                _unitOfWork.Product.Update(product);

               
            }


           
            await _unitOfWork.Order.Add(order);
            int result = await _unitOfWork.SaveAsync();


                model.IsSuccess = result > 0;
                model.Message = result > 0 ? "Order created successfully." : "Failed to create order.";
                model.Data = order;

            return model;
         
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

        public async Task<OrderListResponseDTO> GetAllOrders()
        {
            var orders = await _unitOfWork.Order.GetAll();

            if (orders is null)
            {
                return new OrderListResponseDTO
                {
                    IsSuccess = false,
                    Message = "No orders found.",
                    Data = null


                };
            }


            return new OrderListResponseDTO
            {
                IsSuccess = orders != null,
                Message = orders != null ? "Orders retrieved." : "No orders found.",
                Data = orders.ToList()
            };

           
        }

        public async Task<OrderListResponseDTO> GetAllOrdersByUserId(int userId)
        {
            OrderListResponseDTO model = new OrderListResponseDTO();
            
            var user = await _unitOfWork.User.GetByIdAsync(userId);

            if (user is null)
            {
                model.IsSuccess = false;
                model.Message = "User not found.";
                model.Data = null;
                return model;
            }
            var orders = await _unitOfWork.Order.GetByCondition(u => u.UserId == userId);

            if (orders is null)
            {
                model.IsSuccess = false;
                model.Message = "No orders found for this user.";
                model.Data = null;
                return model;
            }

            model.IsSuccess = true;
            model.Message = "Orders retrieved successfully.";
            model.Data = orders.ToList();

            return model;
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
            var productName = await _unitOfWork.Product.GetByCondition(p => p.Name == requestDTO.ProductName);
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
            order.Price = product.Price;
            order.Profit = product.Profit ;
            order.TotalPrice = product.Price * requestDTO.Quantity;
            order.TotalProfit = product.Profit * requestDTO.Quantity;
                
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
