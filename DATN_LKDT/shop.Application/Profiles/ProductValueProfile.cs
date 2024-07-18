using AutoMapper;
using shop.Application.ViewModels.RequestDTOs.ProductValueDto;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Profiles
{
    public class ProductValueProfile : Profile
    {
        public ProductValueProfile()
        {
            CreateMap<AddProductValueDto, ProductValue>();
            CreateMap<UpdateProductValueDto, ProductValue>();
        }
    }
}
