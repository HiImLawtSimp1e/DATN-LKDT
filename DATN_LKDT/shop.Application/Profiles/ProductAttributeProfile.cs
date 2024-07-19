using AutoMapper;
using shop.Application.ViewModels.RequestDTOs.ProductAttributeDto;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Profiles
{
    public class ProductAttributeProfile : Profile
    {
        public ProductAttributeProfile()
        {
            CreateMap<AddUpdateProductAttributeDto, ProductAttribute>();
        }
    }
}
