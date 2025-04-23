using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.ApplicationConfig;
using MODEL.Entities;

namespace MODEL.DTOs;

public class OrderResponseDTO : Common
{

    public Order Data { get; set; }
}

public class OrderListResponseDTO : Common
{
 public List<Order> Data { get; set; }   
}
