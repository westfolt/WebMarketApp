using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BLL.Dto;
using DAL.Entities;

namespace BLL.Infrastructure
{
    public class WebMarketMapperProfile:Profile
    {
        public WebMarketMapperProfile()
        {
            CreateMap<ProductCategory, ProductCategoryDto>()
                .ForMember(dto => dto.ProductsIds, pc => pc.MapFrom(x => x.Products.Select(p => p.Id)))
                .ReverseMap();

            CreateMap<Product, ProductDto>()
                .ForMember(dto => dto.OrderDetailsIds, p => p.MapFrom(x => x.OrderDetails.Select(od => od.Id)))
                .ForMember(dto => dto.CategoryName, p => p.MapFrom(x => x.Category.CategoryName))
                .ReverseMap();

            CreateMap<OrderDetail, OrderDetailDto>()
                .ForMember(dto => dto.ProductPrice, od => od.MapFrom(x => x.UnitPrice))
                .ReverseMap();

            CreateMap<Order, OrderDto>()
                .ForMember(dto => dto.OrderDetailsIds, o => o.MapFrom(x => x.OrderDetails.Select(od => od.Id)))
                .ForMember(dto => dto.OrderStatus, o => o.MapFrom(x => x.OrderStatus))
                .ReverseMap();

            CreateMap<Customer, CustomerDto>()
                .ForMember(dto => dto.Name, c => c.MapFrom(x => x.Person.Name))
                .ForMember(dto => dto.Surname, c => c.MapFrom(x => x.Person.Surname))
                .ForMember(dto => dto.BirthDate, c => c.MapFrom(x => x.Person.BirthDate))
                .ForMember(dto => dto.Email, c => c.MapFrom(x => x.Person.Email))
                .ForMember(dto => dto.Phone, c => c.MapFrom(x => x.Person.Phone))
                .ForMember(dto => dto.OrdersIds, c => c.MapFrom(x => x.Orders.Select(o => o.Id)))
                .ReverseMap();
            
        }
    }
}
