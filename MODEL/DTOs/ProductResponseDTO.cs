using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.ApplicationConfig;
using MODEL.Entities;

namespace MODEL.DTOs;

public class ProductResponseDTO : Common
{
    public Product Data { get; set; }
}

public class ProductListResponseDTO : Common
{
    public List<Product> Data { get; set; }
}
