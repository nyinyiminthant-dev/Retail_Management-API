using MODEL.ApplicationConfig;
using MODEL.Entities;

namespace MODEL.DTOs;

public class CategoryResponseDTO : Common
{
    public Category Data { get; set; }
}


public class CategoryListResponseDTO : Common
{
    public List<Category> Data { get; set; }
}