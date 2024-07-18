using AutoMapper;
using shop.Application.ViewModels.RequestDTOs.ProductImageDto;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Profiles
{
    public class ProductImageProfile : Profile
    {
        public ProductImageProfile() 
        {
            CreateMap<AddProductImageDto, ProductImage>();
            CreateMap<UpdateProductImageDto, ProductImage>();
        }
    }
}
