using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.IServices;
using MODEL.DTOs;
using REPOSITORY.UnitOfWork;

namespace BAL.Services;

public class ConFirmOrderService : IConfirmOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public ConFirmOrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<OrderResponseDTO> ConfirmOrder(int id)
    {
        OrderResponseDTO model = new OrderResponseDTO();

        var order = await _unitOfWork.Order.GetById(id);
        if (order == null)
        {
            model.IsSuccess = false;
            model.Message = "Order not found.";
            model.Data = null;
            return model;
        }

       
        order.IsOrder = "Confirmed";

        _unitOfWork.Order.Update(order);
        int result = await _unitOfWork.SaveAsync();

        model.IsSuccess = result > 0;
        model.Message = result > 0 ? "Order confirmed successfully." : "Failed to confirm order.";
        model.Data = order;

        return model;
    }


}
