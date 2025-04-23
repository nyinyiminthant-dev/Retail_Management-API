using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.DTOs;

namespace BAL.IServices;

public interface IOrderService
{
    Task<OrderListResponseDTO> GetAllOrders( );

    Task<OrderListResponseDTO> GetAllOrdersByUserId(int userId);

    Task<OrderResponseDTO> GetOrderById(int id);
    Task<OrderResponseDTO> InsertOrder(OrderRequestDTO requestDTO);
    Task<OrderResponseDTO> UpdateOrder(int id, OrderRequestDTO requestDTO);
    Task<OrderResponseDTO> DeleteOrder(int id);
}
   