using AutoMapper;
using shop.Application.ViewModels.RequestDTOs;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            //Map DTOs to Entity
            CreateMap<StoreCartItemDto, CartItem>()
                .ForMember(dest => dest.CartId, opt => opt.Ignore());
        }
    }
}
