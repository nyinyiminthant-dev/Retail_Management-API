using AutoMapper;
using MODEL.DTOs;
using MODEL.Entities;

namespace Retail_Management_API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductRequestDTO>().ReverseMap();
            
            CreateMap<Order,OrderResponseDTO>().ReverseMap();

        }
    }
}
