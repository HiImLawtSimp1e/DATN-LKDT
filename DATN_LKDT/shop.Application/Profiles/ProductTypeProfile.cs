using AutoMapper;
using shop.Application.ViewModels.RequestDTOs.ProductTypeDto;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Profiles
{
    public class ProductTypeProfile : Profile
    {
        public ProductTypeProfile()
        {
            CreateMap<AddUpdateProductTypeDto, ProductType>();
        }
    }
}
