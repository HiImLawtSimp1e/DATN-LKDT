using AutoMapper;
using shop.Application.ViewModels.RequestDTOs;
using shop.Application.ViewModels.RequestDTOs.DiscountDto;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Profiles
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            //Mapping DTOs to Entity
            CreateMap<AddDiscountDto, DiscountEntity>();
            CreateMap<UpdateDiscountDto, DiscountEntity>();
        }
    }
}
