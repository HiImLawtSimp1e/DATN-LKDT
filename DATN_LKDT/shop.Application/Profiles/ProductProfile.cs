using AutoMapper;
using shop.Application.ViewModels.RequestDTOs.ProductAttributeDto;
using shop.Application.ViewModels.RequestDTOs.ProductDto;
using shop.Application.ViewModels.ResponseDTOs.CustomerProductResponseDto;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            // Map Product list(Entity) to customer product list(DTO)
            CreateMap<Product, CustomerProductResponseDto>();
            CreateMap<ProductVariant, ProductVariantDto>();
            CreateMap<ProductType, ViewModels.ResponseDTOs.CustomerProductResponseDto.ProductTypeDto>();
            CreateMap<ProductValue, ProductValueDto>();
            CreateMap<ProductAttribute, ProductAttributeDto>();
            // Map DTO to entity
            CreateMap<AddProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}
